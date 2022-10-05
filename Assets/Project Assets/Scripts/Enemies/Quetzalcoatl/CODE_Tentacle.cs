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

    private int bodyLength;
    private Vector3[] _segmentPoses;
    private Vector3[] _segmentV;
    private Transform[] _bodyParts;


    private void Start()
    {
        DefineBodyLength();
        CreateEnemyBody();
        SetBodyTargets();
    }

    private void Update()
    {
        RefreshPosition();
    }


    /// <summary>
    /// Define enemy body length
    /// </summary>
    private void DefineBodyLength()
    {
        for (int i = 0; i < bodyGameObjects.Length; i++)
        {
            bodyLength += bodyGameObjects[i].bodyPos.Length;
        }
        _bodyParts = new Transform[bodyLength];
    }

    /// <summary>
    /// Create enemy body
    /// </summary>
    private void CreateEnemyBody()
    {
        int k;
        // For all the body parts on the enemy
        for (int i = 0; i < _bodyParts.Length; i++)
        {
            // Check the struct containing the Gameobjects and their positions on the body
            for (int j = 0; j < bodyGameObjects.Length; j++)
            {
                k = 0;

                // Use the position array length to define how much of that body part to instantiate
                while (k < bodyGameObjects[j].bodyPos.Length)
                {
                    // Check if the actual position on the body is equal to the position desired for a certain body part on the struct
                    if (i + 1 == bodyGameObjects[j].bodyPos[k])
                    {
                        // If so, instantiate it and go to the next body part
                        InstantiateBodyParts(i, bodyGameObjects[j].bodyGO);
                        i++;
                    }

                    k++;
                }
            }
        }
    }

    /// <summary>
    /// Set body targets
    /// </summary>
    private void SetBodyTargets()
    {
        // Assign the head transformer as a target for the first body part
        _bodyParts[0].gameObject.GetComponent<CODE_BodyRotation>().target = transform.parent;

        // Assign each subsequent body part as a target to a previous body part on the array
        for (int i = 1; i < _bodyParts.Length; i++)
        {
            _bodyParts[i].gameObject.GetComponent<CODE_BodyRotation>().target = _bodyParts[i - 1].transform;
        }

        _segmentPoses = new Vector3[bodyLength + 1];
        _segmentV = new Vector3[bodyLength + 1];
    }

    /// <summary>
    /// Instantiate body parts
    /// </summary>
    private void InstantiateBodyParts(int Count, GameObject BodyPart)
    {
        _bodyParts[Count] = Instantiate(BodyPart, new Vector2(transform.parent.position.x, transform.parent.position.y), Quaternion.identity).transform;
    }

    /// <summary>
    /// Refresh segment points positions
    /// </summary>
    private void RefreshPosition()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        _segmentPoses[0] = targetDir.position;

        // Refresh segment points positions for the body parts to follow it
        for (int i = 1; i < _segmentPoses.Length; i++)
        {
            Vector3 targetPos = _segmentPoses[i - 1] + (_segmentPoses[i] - _segmentPoses[i - 1]).normalized * targetDist;
            _segmentPoses[i] = Vector3.SmoothDamp(_segmentPoses[i], targetPos, ref _segmentV[i], smoothSpeed);
            _bodyParts[i - 1].transform.position = _segmentPoses[i];
        }
    }

   
}
