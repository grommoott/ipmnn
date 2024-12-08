using UnityEngine;
using TMPro;
using Interacting;
using Player;
using System.Text;
using Helpers;

namespace UI
{
    public class Overlay : MonoBehaviour
    {
        [SerializeField] private RectTransform _interactingsBackground;
        [SerializeField] private float _interactingsPadding;
        [SerializeField] private TMP_Text _interactingsText;
        [SerializeField] private float _interactingHeight;
        [SerializeField] private Bar _stamineBar;
        [SerializeField] private Bar _saturationBar;

        private RectTransform _interactingsTextRect;

        private void Start()
        {
            _interactingsTextRect = _interactingsText.GetComponent<RectTransform>();
            _stamineBar.SetMinMax(PlayerController.Instance.StateManager.StamineMinMax);
            _saturationBar.SetMinMax(PlayerController.Instance.StateManager.SaturationMinMax);
        }

        private void Update()
        {
            _stamineBar.SetValue(PlayerController.Instance.StateManager.Stamine);
            _saturationBar.SetValue(PlayerController.Instance.StateManager.Saturation);

            Interaction def = PlayerController.Instance.InteractingManager.OnDefaultInteraction;
            Interaction lmb = PlayerController.Instance.InteractingManager.OnLMBInteraction;
            Interaction rmb = PlayerController.Instance.InteractingManager.OnRMBInteraction;

            _interactingsBackground.gameObject.SetActive(!(def == null && lmb == null && rmb == null));

            StringBuilder builder = new StringBuilder();
            int interactings = 0;

            if (def != null)
            {
                interactings++;
                builder.AppendJoin("\n", $"F - {def.Name}");
            }

            if (lmb != null)
            {
                interactings++;
                builder.AppendJoin("\n", $"ЛКМ - {lmb.Name}");
            }

            if (rmb != null)
            {
                interactings++;
                builder.AppendJoin("\n", $"ПКМ - {rmb.Name}");
            }

            _interactingsText.text = builder.ToString();
            _interactingsBackground.sizeDelta =
                new Vector2
                (
                    _interactingsTextRect.sizeDelta.x + _interactingsPadding * 2,
                    _interactingHeight * interactings + _interactingsPadding * 2
                );

            _interactingsTextRect.sizeDelta =
                new Vector2
                (

                    _interactingsTextRect.sizeDelta.x,
                    _interactingHeight * interactings
                );
        }
    }
}
