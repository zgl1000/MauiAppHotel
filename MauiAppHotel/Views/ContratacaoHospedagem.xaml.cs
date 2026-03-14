using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
	App PropriedadesApp;
	public ContratacaoHospedagem()
	{
		InitializeComponent();

		PropriedadesApp = (App)Application.Current;

		pck_quarto.ItemsSource = PropriedadesApp.listaQuartos;

		dtpck_checkin.MinimumDate = DateTime.Now;
		dtpck_checkin.MaximumDate = DateTime.Now.AddMonths(1);


		dtpck_checkout.MinimumDate = dtpck_checkin.Date.Value.AddDays(1);
		dtpck_checkout.MaximumDate = dtpck_checkin.Date.Value.AddMonths(6);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		try
		{
			Hospedagem hospedagem = new Hospedagem()
			{
				QuartoSelecionado = (Quarto)pck_quarto.SelectedItem,
				QntAdultos = Convert.ToInt32(stp_adultos.Value),
				QntCriancas = Convert.ToInt32(stp_criancas.Value),
				DataCheckIn = dtpck_checkin.Date.GetValueOrDefault(),
				DataCheckOut = dtpck_checkout.Date.GetValueOrDefault()
            };

            await Navigation.PushAsync(new HospedagemContratada()
			{
				BindingContext = hospedagem
			});
		}
		catch (Exception ex)
		{
            await DisplayAlertAsync("Ops", ex.Message, "OK");
		}
    }

    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
        DatePicker elemento = sender as DatePicker;

		DateTime data_selecionada_chekckin = elemento.Date.GetValueOrDefault();

		dtpck_checkout.MinimumDate = data_selecionada_chekckin.AddDays(1);
		dtpck_checkout.MinimumDate = data_selecionada_chekckin.AddMonths(6);
    }
}