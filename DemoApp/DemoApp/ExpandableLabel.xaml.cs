using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpandableLabel : ContentView
    {
        private bool _IsExpanded;
        private bool _IsExpanding;

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: "Title",
            returnType: typeof(string),
            declaringType: typeof(ExpandableLabel),
            defaultValue: default(string));

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(string),
            declaringType: typeof(ExpandableLabel),
            defaultValue: default(string));

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public ExpandableLabel()
        {
            InitializeComponent();
            ExpandableContent.HeightRequest = 0;
            ExpandableText.Text = Text;
            TitleText.Text = Title;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TitleProperty.PropertyName)
            {
                TitleText.Text = Title;
            }
            else if (propertyName == TextProperty.PropertyName)
            {
                ExpandableText.Text = Text;
            }
        }

        private async void Title_Clicked(object sender, EventArgs e)
        {
            if (!_IsExpanding)
            {
                _IsExpanding = true;
                var height = AuxContainer.Height;
                if (_IsExpanded)
                {
                    var animation = new Animation(v => ExpandableContent.HeightRequest = v, height, 0);
                    await ExpandableContent.FadeTo(0, 250);
                    animation.Commit(this, "ExpandSize", 16, 250);

                }
                else
                {
                    var animation = new Animation(v => ExpandableContent.HeightRequest = v, 0, height);
                    animation.Commit(this, "ExpandSize", 16, 250);
                    await ExpandableContent.FadeTo(1, 250);
                }
                _IsExpanded = !_IsExpanded;
                _IsExpanding = false;
            }
        }
    }
}