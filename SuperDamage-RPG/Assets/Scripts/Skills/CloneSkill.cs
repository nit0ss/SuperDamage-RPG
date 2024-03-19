using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skills
{

    [Header("Clone Info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [SerializeField] private bool canAttack;

    public void CreateClone(Transform _clonePosition)
    {
        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<CloneSkillController>().SetUpClone(_clonePosition,cloneDuration,canAttack);

    }


}
