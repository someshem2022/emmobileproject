

namespace EM.InsurePlus.Controls
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class FrameShadowView : Frame
    {
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create<FrameShadowView, double>(p => p.CornerRadius, 0);

        public double CornerRadius
        {
            get { return (double)base.GetValue(CornerRadiusProperty); }
            set { base.SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty StrokeProperty =
            BindableProperty.Create<FrameShadowView, Color>(p => p.Stroke, Color.Transparent);

        public Color Stroke
        {
            get { return (Color)base.GetValue(StrokeProperty); }
            set { base.SetValue(StrokeProperty, value); }
        }

        public static readonly BindableProperty StrokeThicknessProperty =
           BindableProperty.Create<FrameShadowView, double>(p => p.StrokeThickness, default(double));

        public double StrokeThickness
        {
            get { return (double)base.GetValue(StrokeThicknessProperty); }
            set { base.SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create<FrameShadowView, bool>(p => p.HasShadow, true);

        public bool HasShadow
        {
            get { return (bool)base.GetValue(HasShadowProperty); }
            set { base.SetValue(HasShadowProperty, value); }
        }

        public static readonly BindableProperty IsRoundedBtnProperty =
            BindableProperty.Create<FrameShadowView, bool>(p => p.IsRoundedBtn, false);

        public bool IsRoundedBtn
        {
            get { return (bool)base.GetValue(IsRoundedBtnProperty); }
            set { base.SetValue(IsRoundedBtnProperty, value); }
        }
    }
}
