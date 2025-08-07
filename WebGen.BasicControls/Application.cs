using System;
using System.Collections.Generic;
using System.Text;

namespace WebGen.BasicControls
{
    /// <summary>
    /// 迟早会被替换成App.g.cs的。
    /// 封装 Avalonia 应用程序的类。
    /// </summary>
    /// <summary>
    /// </summary>
    /// <remarks>
    /// Application 类封装了 Avalonia 应用程序相关的功能，包括：
    /// - 一组全局数据模板（DataTemplates）。
    /// - 一组全局样式集合（Styles）。
    /// - 应用的焦点管理器（FocusManager）。
    /// - 应用的输入管理器（InputManager）。
    /// - 在 RegisterServices 方法中注册 Avalonia 其他部分所需的服务。
    /// - 管理应用程序生命周期。
    /// </remarks>
    public class Application : DependencyObject, IDataContextProvider, IGlobalDataTemplates, IGlobalStyles, IThemeVariantHost, IResourceHost2, IApplicationPlatformEvents, IOptionalFeatureProvider
    {
        /// <summary>
        /// 应用程序全局的数据模板集合。
        /// </summary>
        private DataTemplates? _dataTemplates;

        private Styles? _styles;

        /// <summary>
        /// 应用程序全局资源字典。
        /// </summary>
        private IResourceDictionary? _resources;

        /// <summary>
        /// 样式添加事件回调。
        /// </summary>
        private Action<IReadOnlyList<IStyle>>? _stylesAdded;

        /// <summary>
        /// 样式移除事件回调。
        /// </summary>
        private Action<IReadOnlyList<IStyle>>? _stylesRemoved;

        /// <summary>
        /// 应用程序生命周期接口。
        /// </summary>
        private IApplicationLifetime? _applicationLifetime;

        private bool _setupCompleted;

        private EventHandler<ResourcesChangedToken>? _resourcesChanged2;

        /// <summary>
        /// 定义 DataContext 属性，指定数据上下文。
        /// </summary>
        public static readonly StyledProperty<object?> DataContextProperty =
            StyledElement.DataContextProperty.AddOwner<Application>();

        /// <summary>
        /// 继承自 ThemeVariantScope，表示当前实际使用的主题变体（如暗色、亮色）。
        /// </summary>
        public static readonly StyledProperty<ThemeVariant> ActualThemeVariantProperty =
            ThemeVariantScope.ActualThemeVariantProperty.AddOwner<Application>();

        /// <summary>
        /// 继承自 ThemeVariantScope，表示请求的主题变体，可能为 null。
        /// </summary>
        public static readonly StyledProperty<ThemeVariant?> RequestedThemeVariantProperty =
            ThemeVariantScope.RequestedThemeVariantProperty.AddOwner<Application>();

        /// <summary>
        /// 资源变更事件，通知订阅者资源已更新。
        /// </summary>
        public event EventHandler<ResourcesChangedEventArgs>? ResourcesChanged;

        /// <summary>
        /// 新版本资源变更事件，使用 ResourcesChangedToken 标识。
        /// </summary>
        event EventHandler<ResourcesChangedToken>? IResourceHost2.ResourcesChanged2
        {
            add => _resourcesChanged2 += value;
            remove => _resourcesChanged2 -= value;
        }

        /// <summary>
        /// 旧版打开 URL 事件，建议改用 IActivatableLifetime 功能。
        /// </summary>
        [Obsolete("Use Application.Current.TryGetFeature<IActivatableLifetime>() instead.")]
        public event EventHandler<UrlOpenedEventArgs>? UrlsOpened;

        /// <summary>
        /// 当实际主题变体发生改变时触发。
        /// </summary>
        public event EventHandler? ActualThemeVariantChanged;

        /// <summary>
        /// 创建 Application 实例，默认名称为 "Avalonia Application"。
        /// </summary>
        public Application()
        {
            Name = "Avalonia Application";
        }

        /// <summary>
        /// 获取或设置应用程序的数据上下文，作为默认数据绑定对象。
        /// </summary>
        public object? DataContext
        {
            get => GetValue(DataContextProperty);
            set => SetValue(DataContextProperty, value);
        }

        /// <summary>
        /// 获取或设置请求的主题变体。
        /// </summary>
        public ThemeVariant? RequestedThemeVariant
        {
            get => GetValue(RequestedThemeVariantProperty);
            set => SetValue(RequestedThemeVariantProperty, value);
        }

        /// <summary>
        /// 获取当前实际应用的主题变体。
        /// </summary>
        public ThemeVariant ActualThemeVariant => GetValue(ActualThemeVariantProperty);

        /// <summary>
        /// 获取当前的 Application 实例。
        /// </summary>
        public static Application? Current => AvaloniaLocator.Current.GetService<Application>();

        /// <summary>
        /// 获取应用程序的全局数据模板集合。
        /// </summary>
        public DataTemplates DataTemplates => _dataTemplates ?? (_dataTemplates = new DataTemplates());

        /// <summary>
        /// 内部使用的输入管理器，管理输入设备事件。
        /// </summary>
        internal InputManager? InputManager { get; private set; }

        /// <summary>
        /// 获取或设置应用程序的全局资源字典。
        /// </summary>
        public IResourceDictionary Resources
        {
            get => _resources ??= new ResourceDictionary(this);
            set
            {
                value = value ?? throw new ArgumentNullException(nameof(value));
                _resources?.RemoveOwner(this);
                _resources = value;
                _resources.AddOwner(this);
            }
        }

        /// <summary>
        /// 获取应用程序的全局样式集合。
        /// </summary>
        /// <remarks>
        /// 全局样式应用于应用内所有窗口。
        /// </remarks>
        public Styles Styles => _styles ??= new Styles(this);

        /// <inheritdoc/>
        bool IDataTemplateHost.IsDataTemplatesInitialized => _dataTemplates != null;

        /// <inheritdoc/>
        bool IResourceNode.HasResources => (_resources?.HasResources ?? false) ||
            (((IResourceNode?)_styles)?.HasResources ?? false);

        /// <summary>
        /// 获取样式宿主的样式父级，这里返回 null，因为 Application 是顶级。
        /// </summary>
        IStyleHost? IStyleHost.StylingParent => null;

        /// <inheritdoc/>
        bool IStyleHost.IsStylesInitialized => _styles != null;

        /// <summary>
        /// 应用程序生命周期接口，用于管理主窗口、退出等操作。
        /// 支持的生命周期包括：
        /// - IClassicDesktopStyleApplicationLifetime（经典桌面应用）
        /// - ISingleViewApplicationLifetime（单视图应用）
        /// - IControlledApplicationLifetime（受控应用）
        /// - IActivatableApplicationLifetime（可激活应用）
        /// </summary>
        public IApplicationLifetime? ApplicationLifetime
        {
            get => _applicationLifetime;
            set
            {
                if (_setupCompleted)
                {
                    throw new InvalidOperationException($"初始化后不能修改 {nameof(ApplicationLifetime)}。");
                }
                _applicationLifetime = value;
            }
        }

        /// <summary>
        /// 获取全局平台相关设置，如系统主题等。
        /// </summary>
        /// <remarks>
        /// 只有应用未初始化时可能为 null。
        /// TopLevel 的 PlatformSettings 是更推荐使用的接口，因为不同窗口可能有不同设置。
        /// </remarks>
        public IPlatformSettings? PlatformSettings => this.TryGetFeature<IPlatformSettings>();

        /// <summary>
        /// 全局样式添加事件。
        /// </summary>
        event Action<IReadOnlyList<IStyle>>? IGlobalStyles.GlobalStylesAdded
        {
            add => _stylesAdded += value;
            remove => _stylesAdded -= value;
        }

        /// <summary>
        /// 全局样式移除事件。
        /// </summary>
        event Action<IReadOnlyList<IStyle>>? IGlobalStyles.GlobalStylesRemoved
        {
            add => _stylesRemoved += value;
            remove => _stylesRemoved -= value;
        }

        /// <summary>
        /// 初始化应用程序（如加载 XAML），可被继承重写。
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// 尝试根据 key 和主题变体获取资源。
        /// </summary>
        public bool TryGetResource(object key, ThemeVariant? theme, out object? value)
        {
            value = null;
            return (_resources?.TryGetResource(key, theme, out value) ?? false) ||
                   Styles.TryGetResource(key, theme, out value);
        }

        /// <summary>
        /// 通知托管资源发生变化，触发资源变化事件。
        /// </summary>
        void IResourceHost.NotifyHostedResourcesChanged(ResourcesChangedEventArgs e)
        {
            _resourcesChanged2?.Invoke(this, ResourcesChangedToken.Create());
            ResourcesChanged?.Invoke(this, e);
        }

        /// <summary>
        /// 通知托管资源变化（使用新的事件版本）。
        /// </summary>
        void IResourceHost2.NotifyHostedResourcesChanged(ResourcesChangedToken token)
        {
            _resourcesChanged2?.Invoke(this, token);
            ResourcesChanged?.Invoke(this, ResourcesChangedEventArgs.Empty);
        }

        /// <summary>
        /// 通知样式被添加。
        /// </summary>
        void IStyleHost.StylesAdded(IReadOnlyList<IStyle> styles)
        {
            _stylesAdded?.Invoke(styles);
        }

        /// <summary>
        /// 通知样式被移除。
        /// </summary>
        void IStyleHost.StylesRemoved(IReadOnlyList<IStyle> styles)
        {
            _stylesRemoved?.Invoke(styles);
        }

        /// <summary>
        /// 注册 Avalonia 所需的内部服务，如焦点管理、输入管理、样式管理等。
        /// </summary>
        public virtual void RegisterServices()
        {
            AvaloniaSynchronizationContext.InstallIfNeeded();

            var focusManager = new FocusManager();
            InputManager = new InputManager();

            // 监听平台颜色值变更事件，用于主题自动切换
            if (PlatformSettings is { } settings)
            {
                settings.ColorValuesChanged += OnColorValuesChanged;
                OnColorValuesChanged(settings, settings.GetColorValues());
            }

            AvaloniaLocator.CurrentMutable
                .Bind<IAccessKeyHandler>().ToTransient<AccessKeyHandler>()
                .Bind<IGlobalDataTemplates>().ToConstant(this)
                .Bind<IGlobalStyles>().ToConstant(this)
                .Bind<IThemeVariantHost>().ToConstant(this)
                .Bind<IFocusManager>().ToConstant(focusManager)
                .Bind<IInputManager>().ToConstant(InputManager)
                .Bind<IToolTipService>().ToConstant(new ToolTipService(InputManager))
                .Bind<IKeyboardNavigationHandler>().ToTransient<KeyboardNavigationHandler>()
                .Bind<IDragDropDevice>().ToConstant(DragDropDevice.Instance);

            // 兼容老版本，默认绑定拖拽源
            if (AvaloniaLocator.Current.GetService<IPlatformDragSource>() == null)
                AvaloniaLocator.CurrentMutable
                    .Bind<IPlatformDragSource>().ToTransient<InProcessDragSource>();

            AvaloniaLocator.CurrentMutable.Bind<IGlobalClock>()
                .ToConstant(MediaContext.Instance.Clock);

            _setupCompleted = true;
        }

        /// <summary>
        /// 框架初始化完成时调用，供子类重写。
        /// </summary>
        public virtual void OnFrameworkInitializationCompleted()
        {
        }

        /// <summary>
        /// 平台事件，通知打开 URL。
        /// </summary>
        void IApplicationPlatformEvents.RaiseUrlsOpened(string[] urls)
        {
            UrlsOpened?.Invoke(this, new UrlOpenedEventArgs(urls));
        }

        private string? _name;

        /// <summary>
        /// 注册 Name 属性，应用程序名称。
        /// </summary>
        public static readonly DirectProperty<Application, string?> NameProperty =
            AvaloniaProperty.RegisterDirect<Application, string?>("Name", o => o.Name, (o, v) => o.Name = v);

        /// <summary>
        /// 获取或设置应用程序名称，平台相关用途。
        /// </summary>
        public string? Name
        {
            get => _name;
            set => SetAndRaise(NameProperty, ref _name, value);
        }

        /// <summary>
        /// 查询是否支持某个功能接口。
        /// 目前支持：
        /// - IPlatformSettings
        /// - IActivatableLifetime
        /// </summary>
        /// <param name="featureType">功能接口类型</param>
        /// <returns>如果支持，返回对应实例；否则返回 null。</returns>
        public object? TryGetFeature(Type featureType)
        {
            if (featureType == typeof(IPlatformSettings))
                return AvaloniaLocator.Current.GetService<IPlatformSettings>();

            if (featureType == typeof(IActivatableLifetime))
                return AvaloniaLocator.Current.GetService<IActivatableLifetime>();

            // 不随便返回其他服务
            return null;
        }

        /// <summary>
        /// 当属性值变化时触发，主要用于更新主题变体属性和通知。
        /// </summary>
        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == RequestedThemeVariantProperty)
            {
                if (change.GetNewValue<ThemeVariant>() is { } themeVariant && themeVariant != ThemeVariant.Default)
                    SetValue(ActualThemeVariantProperty, themeVariant);
                else
                    ClearValue(ActualThemeVariantProperty);
            }
            else if (change.Property == ActualThemeVariantProperty)
            {
                ActualThemeVariantChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 监听平台颜色变化事件，自动切换主题变体。
        /// </summary>
        private void OnColorValuesChanged(object? sender, PlatformColorValues e)
        {
            SetValue(ActualThemeVariantProperty, (ThemeVariant)e.ThemeVariant, BindingPriority.Template);
        }
    }
}
