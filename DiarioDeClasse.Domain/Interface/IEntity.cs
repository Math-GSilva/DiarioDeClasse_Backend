namespace DiarioDeClasse.Domain.Interface
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
