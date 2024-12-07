using UnityEngine;
using Items.Storages;
using Items;
using Items.Shell;
using System.Collections;
using Saves;
using Newtonsoft.Json.Linq;

namespace Player
{
    public class PlayerInventoryManager : MonoBehaviour, ISaveable
    {
        [SerializeField] private Transform _handSlotTransform;
        private GameObject _handSlotItem;

        private PlayerController _player;
        public PlayerController Player
        {
            get
            {
                return _player;
            }
        }

        private PlayerInventory _inventory;
        public PlayerInventory Inventory { get { return _inventory; } }

        public System.Collections.ObjectModel.ReadOnlyCollection<Item> ItemList
        {
            get
            {
                return _inventory.Items;
            }
        }

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
            _inventory = new PlayerInventory();

            _inventory.AddItem(ItemManager.GetById(ItemIds.EnergyHoney, 10));
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SavesManager.Instance.IsGameLoaded);

            UpdateSlotItemsGO(PlayerInventorySlot.Hand);
        }

        public void AddItem(Item item)
        {
            item = _inventory.AddItem(item);

            if (item != null)
            {
                ItemShellManager.Instance.SpawnItem(item, transform.position);
            }

            SavesManager.Instance.SaveField(this);
        }

        public Item AddItemWithRemainder(Item item)
        {
            Item result = _inventory.AddItem(item);
            SavesManager.Instance.SaveField(this);
            return result;
        }

        public Item GetItem(string id, int count)
        {
            Item result = _inventory.GetItem(id, count);
            SavesManager.Instance.SaveField(this);
            return result;
        }

        public void AddItemInSlot(PlayerInventorySlot slot, Item item)
        {
            item = _inventory.AddItemInSlot(slot, item);

            if (item != null)
            {
                ItemShellManager.Instance.SpawnItem(item, transform.position);
            }

            SavesManager.Instance.SaveField(this);
        }

        public Item GetItemInSlot(PlayerInventorySlot slot, int count)
        {
            Item result = _inventory.GetItemInSlot(slot, count);
            SavesManager.Instance.SaveField(this);
            return result;
        }

        private void UpdateSlotItemsGO(PlayerInventorySlot slot)
        {
            switch (slot)
            {
                case PlayerInventorySlot.Hand:
                    Destroy(_handSlotItem);
                    _handSlotItem = null;

                    Item inHand;

                    if (_inventory.PlayerSlots.TryGetValue(PlayerInventorySlot.Hand, out inHand))
                    {
                        _handSlotItem = Instantiate
                            (
                                Resources.Load<GameObject>("Models/Items/" + inHand.Id),
                                _handSlotTransform
                            );
                    }
                    break;

                default:
                    break;
            }
        }

        public void Load(JObject data)
        {
            if (data == null)
            {
                return;
            }

            Serialization.PlayerInventory playerInventory = data.ToObject<Serialization.PlayerInventory>();

            _inventory = PlayerInventory.Parse(playerInventory);
        }

        public object Save()
        {
            return _inventory.Serialize();
        }

        public string GetSavingPath() => "inventory";
    }
}
