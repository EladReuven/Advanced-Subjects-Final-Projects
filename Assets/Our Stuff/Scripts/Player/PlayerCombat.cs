using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _weaponSocket;
    private float _attackSpeed;
    private float _attackDamage;
    private string _attackAnimation;
    private Animator _anim => GetComponentInChildren<Animator>();

    private bool _isAttacking = false;
    private Camera _camera => Camera.main;
    private NavMeshAgent _navMeshAgent => GetComponent<NavMeshAgent>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !_isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            StartCoroutine(RotatePlayer(hit.point));
        }
    }

    private IEnumerator RotatePlayer(Vector3 targetPoint)
    {
        _isAttacking = true;
        _navMeshAgent.updateRotation = false;

        _anim.SetTrigger(_attackAnimation);

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

        // Ignore rotation on X and Z axes
        targetRotation.x = 0f;
        targetRotation.z = 0f;

        float rotationTime = Quaternion.Angle(startRotation, targetRotation) / _navMeshAgent.angularSpeed;
        float attackTime = _attackSpeed - rotationTime;

        if (rotationTime > 0f)
        {
            float elapsedTime = 0f;

            while (elapsedTime < rotationTime)
            {
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / rotationTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = targetRotation;
        }

        if (attackTime > 0f)
        {
            yield return new WaitForSeconds(attackTime);
        }

        _navMeshAgent.updateRotation = true;
        _isAttacking = false;
    }

    public void SwitchWeapon(SOweapon newWeapon)
    {
        if (_weaponSocket.childCount > 0)
        {
            Destroy(_weaponSocket.GetChild(0).gameObject);
        }
        Instantiate(newWeapon.WeaponPrefab, _weaponSocket.transform);
        _attackDamage = newWeapon.Damage;
        _attackSpeed = newWeapon.AttackSpeed;
        _attackAnimation = newWeapon.Animation;
    }
}
