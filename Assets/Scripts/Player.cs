using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;


[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] public List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private float _timeBetweenShots;
    public float startTimeBetweenShots;
    [SerializeField] private GameObject _deathScene;
    public List<Weapon> AllWeapons;

    [HideInInspector] public int _currentWeaponNumber = 0;
    [HideInInspector] public Weapon _currentWeapon;
    [HideInInspector] public int _currentHealth;

    private Animator _animator;

    public int Money { get; private set; }
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    


    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_timeBetweenShots <= 0)
        {

            if (Input.GetMouseButtonDown(0))
            {
                _currentWeapon.Shoot(_shootPoint);
                _timeBetweenShots = startTimeBetweenShots;
            }
        }
        _timeBetweenShots -= Time.deltaTime;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);
        if (_currentHealth <= 0)
        {
            _animator.Play("Player dead");
            enabled = false;
            _deathScene.SetActive(true);
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }

    public void SaveToProgress()
    {
        Progress.Instance.PlayerInfo.Money = Money; // Сохраняем монеты

        var weaponList = new List<WeaponItem>(); 

        foreach (var weapon in _weapons)
        {
            var temp = new WeaponItem();
            temp.WeaponId = weapon.WeaponId;
            temp.IsPlayerHas = weapon.IsBuyed;
            weaponList.Add(temp);
        }

        Progress.Instance.PlayerInfo.WeaponItems = weaponList; // Сохраняем оружие 
    }

    public void AddWeaponToList(WeaponItem weaponItem)
    {
        for (int i = 0; i < AllWeapons.Count; i++)
        {

            if(AllWeapons[i].WeaponId == weaponItem.WeaponId)
            {
                _weapons.Add(AllWeapons[i]);
                return;
            }
        }
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if(_currentWeaponNumber == _weapons.Count - 1)
        {
            _currentWeaponNumber = 0;
        }
        else
        {
            _currentWeaponNumber++;
        }

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
        {
            _currentWeaponNumber = _weapons.Count - 1;
        }
        else
        {
            _currentWeaponNumber--;
        }

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}
