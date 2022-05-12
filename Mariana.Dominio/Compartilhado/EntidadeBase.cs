namespace Marina.Dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public int Numero { get; set; }
        public string Titulo { get; set; }

        public abstract void Atualizar(T registro);

    }
}
