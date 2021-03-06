using UnityEngine;
using UnityEngine.UI;

namespace Audio {
    
    [RequireComponent(typeof(Button))]
    public class AudioButton : MonoBehaviour {

        private Button _button;

        private void Awake() {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick() {
            MusicManager.Instance.click();
        }
    }
}