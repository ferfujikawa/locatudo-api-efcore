namespace Locatudo.Compartilhado.ObjetosDeValor
{
    public class HorarioLocacao
    {
        public HorarioLocacao(DateTime inicio)
        {
            Inicio = new DateTime(inicio.Year, inicio.Month, inicio.Day, inicio.Hour, 0, 0);
        }

        public DateTime Inicio { get; private set; }
    }
}
