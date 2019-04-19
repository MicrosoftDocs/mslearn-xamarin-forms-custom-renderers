using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFDraw
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        bool IsCanvasDirty
        {
            get { return isCanvasDirty; }
            set
            {
                isCanvasDirty = value;

                if (clearCommand != null)
                    clearCommand.ChangeCanExecute();
            }
        }
        bool isCanvasDirty;

        Command clearCommand;

        public MainPage()
        {
            InitializeComponent();

            sketchView.SketchUpdated += OnSketchUpdated;

            clearCommand = new Command(OnClearClicked, () => { return IsCanvasDirty; });
            
            var trash = new ToolbarItem()
            {
                Text = "Clear",
                Icon = "trash.png",
                Command = clearCommand
            };
            ToolbarItems.Add(trash);

            ToolbarItems.Add(new ToolbarItem("New Color", "pencil.png", OnColorClicked));
        }

        void OnSketchUpdated(object sender, EventArgs e)
        {
            IsCanvasDirty = true;
        }

        void OnClearClicked()
        {
            sketchView.Clear();
            IsCanvasDirty = false;
        }

        private void OnColorClicked()
        {
            sketchView.InkColor = GetRandomColor();
        }
        
        Random rand = new Random();
        Color GetRandomColor()
        {
            return new Color(rand.NextDouble(), rand.NextDouble(), rand.NextDouble());
        }
    }
}
