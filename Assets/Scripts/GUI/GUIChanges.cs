using TMPro;
using UnityEngine;

namespace GrandWarStrategy.Utility
{
    public class GUIChanges : MonoBehaviour
    {
        public void showPanel(GameObject panel)
        {
            panel.SetActive(true);
        }

        public void hidePanel(GameObject panel)
        {
            panel.SetActive(false);
        }

        public void updateText(TMP_Text text, string originalText, string newText)
        {
            text.text = originalText + newText;
        }
    }
}