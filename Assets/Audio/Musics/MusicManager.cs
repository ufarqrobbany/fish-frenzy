using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private AudioSource audioSource;
    private bool isMusicOn = true; // Status awal musik menyala

    [SerializeField] private Sprite musicOnSprite;  // Sprite untuk musik ON
    [SerializeField] private Sprite musicOffSprite; // Sprite untuk musik OFF
    [SerializeField] private Button musicButton;    // Referensi ke tombol musik

    private void Awake()
    {
        // Singleton untuk memastikan hanya ada satu instance MusicManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancurkan saat scene berubah
        }
        else
        {
            Destroy(gameObject); // Hancurkan duplikat
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Pastikan sprite awal sesuai status musik
        UpdateButtonSprite();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn; // Ubah status musik
        if (isMusicOn)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        // Jika tombol UI ada di scene, perbarui sprite tombol
        if (musicButton != null)
        {
            musicButton.image.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
        }
    }

    // Method untuk menghubungkan tombol baru di scene lain
    public void AssignButton(Button newButton)
    {
        musicButton = newButton;
        UpdateButtonSprite();
        newButton.onClick.AddListener(ToggleMusic);
    }
}
