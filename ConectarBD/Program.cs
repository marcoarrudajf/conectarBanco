namespace ConectarBD
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            ConectarBDados conexaoBD = new ConectarBDados();
            conexaoBD.conectarBDados();
        }
    }
}