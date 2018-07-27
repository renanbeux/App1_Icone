using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1.Servico;
using App1.Servico.Modelo;

namespace App1
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            Botao.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                        Resultado.Text = string.Format("CEP: {0} \nEndereço: {1} {2} \nBairro: {3} \nCidade: {4} - {5} \nIBGE: {6}", end.cep, end.logradouro, end.complemento, end.bairro, end.localidade, end.uf, end.ibge);
                    else
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                }
                catch(Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;
            
            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! \nO CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }

            if (!int.TryParse(cep, out int NovoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! \nO CEP deve ser composto apenas por números.", "OK");
                valido = false;
            }

            return valido;
        }
	}
}
