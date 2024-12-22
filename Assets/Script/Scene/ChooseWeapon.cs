using UnityEngine;
using UnityEngine.UIElements;

public class ChooseWeapon : MonoBehaviour
{
    public OrbWeapon orbWeapon;
    public WeaponRocketHolder rocketWeapon;
    
    public int pointLeft = 3;
    private Label pointLeftText;
    private Label orbLevelText;
    private Label rocketLevelText;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button weapon1MinusButton = root.Q<Button>("1Minus");
        Button weapon1PlusButton = root.Q<Button>("1Plus");
        Button weapon2MinusButton = root.Q<Button>("2Minus");
        Button weapon2PlusButton = root.Q<Button>("2Plus");

        Button okButton = root.Q<Button>("ConfirmButton");
        pointLeftText = root.Q<Label>("Point");
        orbLevelText = root.Q<Label>("Weapon1Description");
        rocketLevelText = root.Q<Label>("Weapon2Description");

        weapon1MinusButton.clicked += () =>
        {
            if (orbWeapon.level > 0)
            {
                orbWeapon.UpgradeWeaponLevel(-1);
                pointLeft++;
            }
        };

        weapon1PlusButton.clicked += () =>
        {
            if (pointLeft > 0)
            {
                orbWeapon.UpgradeWeaponLevel(1);
                pointLeft--;
            }
        };

        weapon2MinusButton.clicked += () =>
        {
            if (rocketWeapon.level > 0)
            {
                rocketWeapon.UpgradeWeaponLevel(-1);
                pointLeft++;
            }
        };

        weapon2PlusButton.clicked += () =>
        {
            if (pointLeft > 0)
            {
                rocketWeapon.UpgradeWeaponLevel(1);
                pointLeft--;
            }
        };

        okButton.clicked += () =>
        {
            GameManager.Instance.InitializeGame();
            GameManager.Instance.LoadBattlefield();
        };    
    }

    private void Update()
    {
        pointLeftText.text = $"{pointLeft}";
        //orbLevelText.text = $"Orb Level: {orbWeapon.level}";   
        //rocketLevelText.text = $"Rocket Level: {rocketWeapon.level}";
    }
}
