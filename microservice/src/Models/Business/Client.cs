using Models.Infrastructure;

namespace Models.Business
{
    public class Client : BaseEntity<int>
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string CPF { get; set; }

    }
}
