using UnityEngine;
using Player;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if (!_instance)
                {
                    Debug.LogWarning("UIManager instance isn't setted");
                }

                return _instance;
            }
        }

        [SerializeField] private GameObject _playerInventoryPage;
        public GameObject PlayerInventoryPage { get { return _playerInventoryPage; } }

        [SerializeField] private GameObject _dialoguePage;
        public GameObject DialoguePage { get { return _dialoguePage; } }

        [SerializeField] private GameObject _questsPage;
        public GameObject QuestsPage { get { return _questsPage; } }

        [SerializeField] private GameObject _shopPage;
        public GameObject ShopPage { get { return _shopPage; } }

        [SerializeField] private GameObject _menuPage;
        public GameObject MenuPage { get { return _menuPage; } }

        [SerializeField] private GameObject _sleepingPage;
        public GameObject SleepingPage { get { return _sleepingPage; } }

        private Page _openedPage = null;

        public Page OpenedPage
        {
            get
            {
                return _openedPage;
            }
        }

        public bool IsPageOpended { get { return _openedPage != null; } }

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
            }
            _instance = this;
        }

        public void OpenPage(Page page)
        {
            if (PlayerController.Instance.SleepingManager.IsSleeping)
            {
                return;
            }

            string _openedPageName = _openedPage?.PageName;
            ClosePage();

            if (page.PageName == _openedPageName)
            {
                Destroy(page.gameObject);
                return;
            }

            _openedPage = page;
            _openedPage.Show();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void ClosePage()
        {
            if (PlayerController.Instance.SleepingManager.IsSleeping)
            {
                return;
            }

            _openedPage?.Close();
            _openedPage = null;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public GameObject InstantiatePage(GameObject page)
        {
            return Instantiate(page, transform);
        }
    }
}
