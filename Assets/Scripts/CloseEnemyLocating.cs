using System.Collections.Generic;
using Character;
using UnityEngine;

public class CloseEnemyLocating : MonoBehaviour
{
    private List<GameObject> _enemies;
    private CharacterMovement _characterMovement;
    private CameraChanging _cameraChanging;
    private const string EnemyTag = "Enemy";
    private void Start()
    {
        _enemies = new List<GameObject>();
        _cameraChanging = FindObjectOfType<CameraChanging>();
        _characterMovement = FindObjectOfType<CharacterMovement>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y,
            _characterMovement.transform.position.z);
    }

    public void RemoveObjectFromEnemies(GameObject enemyObject)
    {
        _enemies.Remove(enemyObject);
        if(_enemies.Count == 0)
            _cameraChanging.ChangeCamera(CameraType.Main);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag(EnemyTag)) return;
        _enemies.Add(other.gameObject);
        _cameraChanging.ChangeCamera(CameraType.Attack);
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.CompareTag(EnemyTag)) return;
        _enemies.Remove(other.gameObject);
        
    }
}
