using UnityEngine;
using UnityEngine.UI;
using Shops;
using Items;
using System.Collections.Generic;
using System;

namespace UI.Pages.Shop
{
    public class TradeElement : MonoBehaviour
    {
        [SerializeField] private TradeItem _tradeItem;
        [SerializeField] private Transform _inputTransform;
        [SerializeField] private Transform _outputTransform;
        [SerializeField] private Button _button;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Color _selectedBackgroundColor;
        [SerializeField] private Color _isntAvailableColor;

        private Trade _trade;

        private void Start()
        {
            if (_trade == null)
            {
                Debug.LogError("Trade wasn't setted in true time");
                return;
            }

            foreach (KeyValuePair<string, int> pair in _trade.Input)
            {
                GameObject tradeItem = Instantiate(_tradeItem.gameObject, _inputTransform);
                tradeItem.GetComponent<TradeItem>().SetItem(ItemManager.GetById(pair.Key, pair.Value));
            }

            foreach (KeyValuePair<string, int> pair in _trade.Output)
            {
                GameObject tradeItem = Instantiate(_tradeItem.gameObject, _outputTransform);
                tradeItem.GetComponent<TradeItem>().SetItem(ItemManager.GetById(pair.Key, pair.Value));
            }
        }

        public void SetTrade(Trade trade)
        {
            _trade = trade;
        }

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                _backgroundImage.color = _selectedBackgroundColor;
            }
        }

        public void SetCallback(Action<string> callback)
        {
            _button.onClick.AddListener(() => callback.Invoke(_trade.Id));
        }

        public void SetAvailable(bool available)
        {
            if (!available)
            {
                _backgroundImage.color = _isntAvailableColor;
            }
        }

        public void Destroy()
        {
            for (int i = 0; i < _inputTransform.childCount; i++)
            {
                GameObject go = _inputTransform.GetChild(i).gameObject;
                go.SetActive(false);
                Destroy(go);
            }

            for (int i = 0; i < _outputTransform.childCount; i++)
            {
                GameObject go = _outputTransform.GetChild(i).gameObject;
                go.SetActive(false);
                Destroy(go);
            }

            Destroy(gameObject);
        }
    }
}
