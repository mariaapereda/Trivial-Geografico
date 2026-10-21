using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace trivialGeografico
{
    public partial class CapitalesPage : ContentPage
    {
        private string[] capitales;
        private string[] paises;

        private int aciertos = 0;
        private int fallos = 0;
        private int preguntasRespondidas = 0;
        private int indiceCorrectoActual;
        private List<int> indicesPreguntados = new List<int>();
        private Random random = new Random();

        public CapitalesPage()
        {
            InitializeComponent();
            InicializarCapitalesPaises();
            CargarSiguientePregunta();
        }

        private void InicializarCapitalesPaises()
        {
            capitales = new string[] { "Berlín", "Viena", "Bruselas", "Sofía", "Nicosia", "Zagreb", "Copenhague", "Bratislava", "Liubliana", "Madrid", "Tallin", "Helsinki", "Paris", "Atenas", "Budapest", "Dublín", "Roma", "Riga", "Vilna", "Luxemburgo", "La Valeta", "Ámsterdam", "Varsovia", "Lisboa", "Londres", "Praga", "Bucarest", "Estocolmo" };
            paises = new string[] { "Alemania", "Austria", "Bélgica", "Bulgaria", "Chipre", "Croacia", "Dinamarca", "Eslovaquia", "Eslovenia", "España", "Estonia", "Finlandia", "Francia", "Grecia", "Hungría", "Irlanda", "Italia", "Letonia", "Lituania", "Luxemburgo", "Malta", "Países Bajos", "Polonia", "Portugal", "Reino Unido", "Chéquia", "Rumanía", "Suecia" };
        }

        private void CargarSiguientePregunta() {

            HabilitarOpciones(true);
            BtnSiguiente.IsVisible = false;

            do
            {
                indiceCorrectoActual = random.Next(capitales.Length);
            } while (indicesPreguntados.Contains(indiceCorrectoActual));

            indicesPreguntados.Add(indiceCorrectoActual);

            PreguntaLabel.Text = $"¿De qué país es la capital {capitales[indiceCorrectoActual]}?";

            List<string> opciones = new List<string> { paises[indiceCorrectoActual] };

            while (opciones.Count < 4)
            {
                int r = random.Next(paises.Length);
                string paisAleatorio = paises[r];

                if (!opciones.Contains(paisAleatorio))
                {
                    opciones.Add(paisAleatorio);
                }
            }

            opciones = opciones.OrderBy(x => random.Next()).ToList();

            BtnOpcion1.Text = opciones[0];
            BtnOpcion2.Text = opciones[1];
            BtnOpcion3.Text = opciones[2];
            BtnOpcion4.Text = opciones[3];
        }

        private async void OnOpcionClicked(object sender, EventArgs e) {
            Button btn = (Button)sender;
            string paisSeleccionado = btn.Text;
            string paisCorrecto = paises[indiceCorrectoActual];

            HabilitarOpciones(false);

            if (paisSeleccionado == paisCorrecto) {
                aciertos++;
                await DisplayAlert("Correcto", "¡Has acertado!", "OK");
            } else {
                fallos++;
                await DisplayAlert("Incorrecto", $"El país correcto es {paisCorrecto}.", "OK");
            }

            preguntasRespondidas++;

            if (preguntasRespondidas >= 10) {
                await DisplayAlert("Juego terminado", $"Aciertos: {aciertos}, Fallos: {fallos}", "OK");
                await Navigation.PopAsync();
            } else {
                BtnSiguiente.IsVisible = true;
            }
        }

        private void OnSiguienteClicked(object sender, EventArgs e) {
            CargarSiguientePregunta();
        }

        private void HabilitarOpciones(bool habilitar) {
            BtnOpcion1.IsEnabled = habilitar;
            BtnOpcion2.IsEnabled = habilitar;
            BtnOpcion3.IsEnabled = habilitar;
            BtnOpcion4.IsEnabled = habilitar;
        }
    }
}