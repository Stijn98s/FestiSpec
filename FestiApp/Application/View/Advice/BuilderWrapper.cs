using System.Windows;
using System.Windows.Forms.Integration;

namespace FestiApp.View.Advice
{
    public partial class BuilderWrapper : WindowsFormsHost
    {
        public Builder Builder = new Builder();

        private static bool _initilized = false;

        public BuilderWrapper()
        {
            Child = Builder;
            Width = Builder.Width;
            Height = Builder.Height;

            InitTextProperty();
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("XML", typeof(string), typeof(BuilderWrapper),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ContentChangedCallback));

        private static void ContentChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && !_initilized)
            {
                ((BuilderWrapper)obj).Builder.Content = (string)e.NewValue;
            }

            _initilized = true;
        }

        private void InitTextProperty()
        {
            Builder.ContentChanged += (sender, e) =>
            {
                SetValue(ContentProperty, this.Builder.Content);
            };
        }

        public string XML
        {
            get => GetValue(ContentProperty) as string;
            set => SetValue(ContentProperty, value);
        }

    }
}
