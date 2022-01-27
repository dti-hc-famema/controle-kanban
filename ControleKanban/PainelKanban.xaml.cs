using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ControleKanban
{
	/// <summary>
	/// Interaction logic for PainelKanban.xaml
	/// </summary>
	public partial class PainelKanban : UserControl
	{
        Int32 tipo_atendimento = 0;

        private const Int32 _SAUDEMENTAL_EMERGENCIA = 43;
        private const Int32 _SAUDEMENTAL_EMERGENCIA_REFERENCIADA = 44;
        private const Int32 _EMERGENCIA = 29;
        private const Int32 _EMERGENCIA_REFERENCIADA = 30;
		private const Int32 _PRE_INTERNACAO = 99;
        private const Int32 _CORONA = 999;

        public PainelKanban()
		{
			this.InitializeComponent();
            this.imgCentroCusto.Source = LoadImage(null);

		}
		
		public static readonly DependencyProperty Texto =
        DependencyProperty.Register("SetTexto", typeof(string), typeof(PainelKanban), new PropertyMetadata("default value"));

		public string SetTexto
		{
			get { return (string)GetValue(Texto); }
			set { SetValue(Texto, value); 
			tbTexto.Text = value;
			}
		}


        public byte[] SetImgCentroCusto
        {
             
            get { return (byte[])((MemoryStream)((BitmapImage)imgCentroCusto.Source).StreamSource).ToArray(); }
            set
            {

                imgCentroCusto.Source = LoadImage(value);
            }
        }


        public Int32 SetBackGround
        {
            get { return tipo_atendimento; }
            set
            {
                tipo_atendimento = value;

                if (tipo_atendimento == _SAUDEMENTAL_EMERGENCIA || tipo_atendimento == _SAUDEMENTAL_EMERGENCIA_REFERENCIADA)
                    pnlKanban.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)); // Laranja mecânica
                else if (tipo_atendimento == _EMERGENCIA || tipo_atendimento == _EMERGENCIA_REFERENCIADA)
                    pnlKanban.Background = new SolidColorBrush(Color.FromArgb(255, 173, 22, 22)); // Vermelho
                else if (tipo_atendimento == _PRE_INTERNACAO)
					pnlKanban.Background = new SolidColorBrush(Color.FromArgb(255, 50, 22, 22)); // Vermelho
                else if (tipo_atendimento == _CORONA)
                    pnlKanban.Background = new SolidColorBrush(Color.FromArgb(255, 131, 60, 194)); //Roxo
                else
                    pnlKanban.Background = new SolidColorBrush(Color.FromArgb(255, 47, 85, 121)); // Azul


                //pnlKanban.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D8E0A627"));
            }
        }
		
		public static readonly DependencyProperty Tamanho =
        DependencyProperty.Register("SetTamanho", typeof(string), typeof(PainelKanban), new PropertyMetadata("default value"));

		public string SetTamanho
		{
			get { return (string)GetValue(Tamanho); }
			set { SetValue(Tamanho, value); 
			tbTexto.FontSize = Convert.ToDouble(value);
			}
		}
		
		

		public static readonly DependencyProperty TipoPac =
        DependencyProperty.Register("SetTipoPac", typeof(string), typeof(PainelKanban), new PropertyMetadata("default value"));

		public string SetTipoPac
		{
			get { return (string)GetValue(Texto); }
			set { SetValue(Texto, value); 
			if(value=="Y")
			{
				pacAMARELO.Visibility = System.Windows.Visibility.Visible;
				pacAZUL.Visibility = System.Windows.Visibility.Hidden;			
				pacVERMELHO.Visibility = System.Windows.Visibility.Hidden;			
				pacVERDE.Visibility = System.Windows.Visibility.Hidden;			
			}
			if(value=="B")
			{
				pacAMARELO.Visibility = System.Windows.Visibility.Hidden;
				pacAZUL.Visibility = System.Windows.Visibility.Visible;			
				pacVERMELHO.Visibility = System.Windows.Visibility.Hidden;			
				pacVERDE.Visibility = System.Windows.Visibility.Hidden;			
			}
			if(value=="G")
			{
				pacAMARELO.Visibility = System.Windows.Visibility.Hidden;
				pacAZUL.Visibility = System.Windows.Visibility.Hidden;			
				pacVERMELHO.Visibility = System.Windows.Visibility.Hidden;			
				pacVERDE.Visibility = System.Windows.Visibility.Visible;			
			}
			if(value=="R")
			{
				pacAMARELO.Visibility = System.Windows.Visibility.Hidden;
				pacAZUL.Visibility = System.Windows.Visibility.Hidden;			
				pacVERMELHO.Visibility = System.Windows.Visibility.Visible;			
				pacVERDE.Visibility = System.Windows.Visibility.Hidden;			
			}
			
			}
		}
		
		public static readonly DependencyProperty Estrela =
        DependencyProperty.Register("SetEstrela", typeof(string), typeof(PainelKanban), new PropertyMetadata("default value"));

		public string SetEstrela
		{
			get { return (string)GetValue(Texto); }
			set { SetValue(Texto, value); 
			if (value =="S")
			{
				estrela.Visibility = pacVERMELHO.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				estrela.Visibility = pacVERMELHO.Visibility = System.Windows.Visibility.Hidden;
			}
			}
		}
		
		public static readonly DependencyProperty Tubo =
        DependencyProperty.Register("SetTubo", typeof(string), typeof(PainelKanban), new PropertyMetadata("default value"));

		public string SetTubo
		{
			get { return (string)GetValue(Texto); }
			set { SetValue(Texto, value); 
			if (value =="V")
			{
				tuboVazio.Visibility = System.Windows.Visibility.Visible;
			}
			if (value =="C")
			{
				tuboCheio.Visibility = System.Windows.Visibility.Visible;
			}
			}
		}
		
		public static void rodar()
		{
				
		}


        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
		
	}
}