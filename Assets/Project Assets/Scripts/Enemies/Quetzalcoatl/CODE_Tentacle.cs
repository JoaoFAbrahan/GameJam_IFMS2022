using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CODE_Tentacle : CODE_BodyRotation
{
    [Serializable]
    public struct BodyPartGameObject
    {
        public GameObject bodyGO;
        public int[] bodyPos;
    }

    private int bodyLength;
    private Vector3[] _segmentPoses;
    private Vector3[] _segmentV;
    private Transform[] _bodyParts;

    [Header("Target")]
    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    [Header("Wiggle")]
    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;

    [Space(10)]
    public BodyPartGameObject[] bodyGameObjects;
    private void Start()
    {
        // Define enemy body length
        for (int i = 0; i < bodyGameObjects.Length; i++)
        {
            bodyLength += bodyGameObjects[i].bodyPos.Length;
        }
        _bodyParts = new Transform[bodyLength];

        createEnemyBody();
        setBodyTargets();
    }

    private void createEnemyBody()
    {
        int k;
        for (int i = 0; i < _bodyParts.Length; i++)
        {
            for (int j = 0; j < bodyGameObjects.Length; j++)
            {
                k = 0;
                while (k < bodyGameObjects[j].bodyPos.Length)
                {
                    if (i + 1 == bodyGameObjects[j].bodyPos[k])
                    {
                        InstantiateBodyParts(i, bodyGameObjects[j].bodyGO);
                        i++;
                    }
                    k++;
                }
            }
        }
    }

    private void setBodyTargets()
    {
        _bodyParts[0].gameObject.GetComponent<CODE_BodyRotation>().target = transform.parent;

        for (int i = 1; i < _bodyParts.Length; i++)
        {
            _bodyParts[i].gameObject.GetComponent<CODE_BodyRotation>().target = _bodyParts[i - 1].transform;
        }

        _segmentPoses = new Vector3[bodyLength + 1];
        _segmentV = new Vector3[bodyLength + 1];
    }

    private void InstantiateBodyParts(int Count, GameObject BodyPart)
    {
        _bodyParts[Count] = Instantiate(BodyPart, new Vector2(transform.parent.position.x, transform.parent.position.y), Quaternion.identity).transform;
    }

    private void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        _segmentPoses[0] = targetDir.position;

        for (int i = 1; i < _segmentPoses.Length; i++)
        {
            Vector3 targetPos = _segmentPoses[i - 1] + (_segmentPoses[i] - _segmentPoses[i - 1]).normalized * targetDist;
            _segmentPoses[i] = Vector3.SmoothDamp(_segmentPoses[i], targetPos, ref _segmentV[i], smoothSpeed);
            _bodyParts[i - 1].transform.position = _segmentPoses[i];
        }
    }
}
