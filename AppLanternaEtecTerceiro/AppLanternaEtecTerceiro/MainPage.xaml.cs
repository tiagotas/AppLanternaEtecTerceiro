using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Battery;

namespace AppLanternaEtecTerceiro
{
    public partial class MainPage : ContentPage
    {
        bool lanterna_ligada = false;

        public MainPage()
        {
            InitializeComponent();

            btnOnOff.Source = ImageSource.FromResource("AppLanternaEtecTerceiro.Img.botao-desligado.jpg");

            Carrega_info_Bateria();
        }

        private void btnOnOff_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(lanterna_ligada)
                {
                    // se estiver ligada, vou desligar.
                    btnOnOff.Source = ImageSource.FromResource("AppLanternaEtecTerceiro.Img.botao-desligado.jpg");
                    lanterna_ligada = false;

                    Flashlight.TurnOffAsync();

                } else
                {
                    // senão, vou ligar.
                    btnOnOff.Source = ImageSource.FromResource("AppLanternaEtecTerceiro.Img.botao-ligado.jpg");
                    lanterna_ligada = true;

                    Flashlight.TurnOnAsync();
                }

                Vibration.Vibrate(TimeSpan.FromMilliseconds(300));

            } catch(Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    

        private void Carrega_info_Bateria()
        {
            try
            {
                if (CrossBattery.IsSupported)
                {
                    CrossBattery.Current.BatteryChanged -= Mudanca_Status_Bateria;
                    CrossBattery.Current.BatteryChanged += Mudanca_Status_Bateria;
                }
                else throw new Exception("Dados da bateria indisponíveis");

            } catch(Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private void Mudanca_Status_Bateria(object sender, Plugin.Battery.Abstractions.BatteryChangedEventArgs e)
        {
            try
            {
                lbl_porcentagem.Text = e.RemainingChargePercent.ToString() + "%";

                if(e.IsLow)
                {
                    lbl_bateria_franca.IsVisible = true;

                } else
                {
                    lbl_bateria_franca.IsVisible = false;
                }

                switch(e.Status)
                {
                    case Plugin.Battery.Abstractions.BatteryStatus.Charging:
                        lbl_status.Text = "Carregando";
                        break;

                    case Plugin.Battery.Abstractions.BatteryStatus.Discharging:
                        lbl_status.Text = "Descarregando";
                        break;

                    case Plugin.Battery.Abstractions.BatteryStatus.Full:
                        lbl_status.Text = "Carregado";
                        break;
                    case Plugin.Battery.Abstractions.BatteryStatus.NotCharging:
                        lbl_status.Text = "Sem Carregar";
                        break;

                    case Plugin.Battery.Abstractions.BatteryStatus.Unknown:
                        lbl_status.Text = "Desconhecido";
                        break;
                }


                switch(e.PowerSource)
                {
                    case Plugin.Battery.Abstractions.PowerSource.Ac:
                        lbl_fonte.Text = "Carregador";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Battery:
                        lbl_fonte.Text = "Bateria";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Usb:
                        lbl_fonte.Text = "USB";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Wireless:
                        lbl_fonte.Text = "Sem fio";
                        break;

                    case Plugin.Battery.Abstractions.PowerSource.Other:
                        lbl_fonte.Text = "Desconhecido";
                        break;
                }

            } catch(Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }

    
    
    
    
    
    }
}
