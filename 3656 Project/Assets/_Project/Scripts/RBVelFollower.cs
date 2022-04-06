using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BezierSolution;
public class RBVelFollower : MonoBehaviour
{
    public BezierSpline spline;
    [SerializeField]
    [Range(0f, 1f)]
    private float m_normalizedT = 0f;
    public float movementLerpModifier = 10f;
    public float travelTime = 5f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float _normalizedT = spline.evenlySpacedPoints.GetNormalizedTAtPercentage(m_normalizedT);

        var thing = spline.GetTangent(_normalizedT);

        Vector2 hold = new Vector2(thing.x, thing.y);

        this.rb.velocity = hold/2f;
        m_normalizedT += Time.fixedDeltaTime / travelTime;
    }
}
