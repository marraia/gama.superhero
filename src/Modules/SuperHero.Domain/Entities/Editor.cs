namespace SuperHero.Domain.Entities
{
    public class Editor
    {
        public Editor(string name)
        {
            Name = name;
        }

        public Editor(int id)
        {
            Id = id;
        }

        public Editor(int id,
                        string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Name))
                valid = false;

            return valid;
        }
    }
}
