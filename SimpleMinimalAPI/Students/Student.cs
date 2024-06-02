namespace SimpleMinimalAPI.Students
{
    public class Student
    {
        public Guid Id { get; init; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }

        public Student(string nome)
        {
            Nome = nome;
            Id = Guid.NewGuid();
            Ativo = true;
        }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }

        public void Desativar()
        {
            Ativo = false;
        }
    }

}
