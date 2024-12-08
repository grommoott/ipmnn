using Items.Interfaces;

namespace Items.Classes.Materials
{
    public class Material : Item, IMaterial
    {
        public Material(int count, string id, string name, string description) : base(count, id, name, description) { }
    }
}
