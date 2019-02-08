using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCepServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereco: {0}-{1}, {2}, {3} = CEP {4}", end.localidade, end.bairro, end.uf, end.logradouro, end.cep);
                    }
                    else
                    {
                        DisplayAlert("ERRO CRÍTICO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");                   }                        
                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }


            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if (string.IsNullOrEmpty(CEP.Text))
            {
                DisplayAlert("ERRO", "CEP inválido! Informe um CEP valido.", "OK");
                valido = false;
            }

            if (CEP.Text.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }

            int NovoCep = 0;
            if (!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter apenas números.", "OK");
                valido = false;
            }
            return valido;
        }
    }
}
