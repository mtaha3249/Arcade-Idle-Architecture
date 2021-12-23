using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utility
{
    /// <summary>
    /// Axis X,Y,Z
    /// </summary>
    public enum Axis
    {
        X = 0,
        Y = 1,
        Z = 2
    }

    /// <summary>
    /// Combine Colors and give new Color
    /// </summary>
    /// <param name="aColors">array of colors</param>
    /// <returns>Combined Color</returns>
    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }

        result /= aColors.Length;
        return result;
    }

    /// <summary>
    /// Ramap OldValue to NewRange
    /// </summary>
    /// <param name="OldValue"></param>
    /// <param name="OldRange"></param>
    /// <param name="NewRange"></param>
    /// <returns>New Value</returns>
    public static float Remap(float OldValue, Vector2 OldRange, Vector2 NewRange)
    {
        return (((OldValue - OldRange.x) * (NewRange.y - NewRange.x)) / (OldRange.y - OldRange.x)) + NewRange.x;
    }

    /// <summary>
    /// Convert Emissive color to Normal color
    /// As Emissive color is HDR so this function will convert HDR to normal color
    /// </summary>
    /// <param name="HDRIntensity">intensity of HDR</param>
    /// <param name="color">color to be converted</param>
    /// <returns>HDR color to normal color</returns>
    public static Color ConvertHDRColor(float HDRIntensity, Color color)
    {
        float factor = Mathf.Pow(2, HDRIntensity);
        Color c = new Color(color.r * factor, color.g * factor, color.b * factor);

        return c;
    }

    /// <summary>
    /// Convert Angle to 0-180
    /// </summary>
    /// <param name="angle">current angle</param>
    /// <returns>converted angle</returns>
    public static float ConvertAngle(float angle)
    {
        float newAngle = 0;
        newAngle = angle > 180 ? angle - 360 : angle;
        return newAngle;
    }

    /// <summary>
    /// Give point of bezair curve
    /// </summary>
    /// <param name="t">time</param>
    /// <param name="start">start point</param>
    /// <param name="mid">mid point</param>
    /// <param name="end">end point</param>
    /// <returns></returns>
    static Vector3 CalculateBazierCurve(float t, Vector3 start, Vector3 mid, Vector3 end)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * start;
        p += 2 * u * t * mid;
        p += tt * end;
        return p;
    }

    /// <summary>
    /// Give point of linear curve
    /// </summary>
    /// <param name="t">time</param>
    /// <param name="start">start point</param>
    /// <param name="end">end point</param>
    /// <returns></returns>
    static Vector3 CalculateLinearCurve(float t, Vector3 start, Vector3 end)
    {
        return start + t * (end - start);
    }

    /// <summary>
    /// Give point of Qaudratic curve
    /// </summary>
    /// <param name="t">time</param>
    /// <param name="start">start point</param>
    /// <param name="mid1">mid point</param>
    /// <param name="mid2">mid point</param>
    /// <param name="end">end point</param>
    /// <returns></returns>
    static Vector3 CalculateCubicCurve(float t, Vector3 start, Vector3 mid1, Vector3 mid2, Vector3 end)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        Vector3 p = uuu * start;
        p += 3 * uu * t * mid1;
        p += 3 * u * tt * mid2;
        p += ttt * end;
        return p;
    }

    /// <summary>
    /// Give whole points of bazier curve
    /// </summary>
    /// <param name="StartPosition">start position</param>
    /// <param name="EndPosition">end point</param>
    /// <param name="smoothPoints">number of points to iterate. More Points, More Smoother</param>
    /// <param name="bazierCurveHeight">height of curve</param>
    /// <returns></returns>
    public static List<Vector3> CalculateBeziarCurve(Vector3 StartPosition, Transform EndPosition,
        int smoothPoints = 30, float bazierCurveHeight = 30)
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i <= smoothPoints; i++)
        {
            Vector3 mid = new Vector3(0, EndPosition.position.y + bazierCurveHeight,
                (StartPosition.z + EndPosition.position.z) / 2);
            Vector3 point =
                CalculateBazierCurve(i / (float) smoothPoints, StartPosition, mid, EndPosition.position);
            points.Add(point);
        }

        return points;
    }

    /// <summary>
    /// Give whole points of Quadratice curve
    /// </summary>
    /// <param name="StartPosition">start position</param>
    /// <param name="EndPosition">end point</param>
    /// <param name="smoothPoints">number of points to iterate. More Points, More Smoother</param>
    /// <param name="bazierCurveHeight">height of curve</param>
    /// <returns></returns>
    public static List<Vector3> CalculateCubicBeziarCurve(Vector3 StartPosition, Transform EndPosition,
        int smoothPoints = 30, float bazierCurveHeight = 30)
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i <= smoothPoints; i++)
        {
            Vector3 mid1 = new Vector3(0, EndPosition.position.y + bazierCurveHeight, StartPosition.z);
            Vector3 mid2 = new Vector3(0, EndPosition.position.y + bazierCurveHeight, EndPosition.position.z);
            Vector3 point = CalculateCubicCurve(i / (float) smoothPoints, StartPosition, mid1, mid2,
                EndPosition.position);
            points.Add(point);
        }

        return points;
    }

    /// <summary>
    /// Give whole points of Linear curve
    /// </summary>
    /// <param name="StartPosition">start position</param>
    /// <param name="EndPosition">end point</param>
    /// <param name="smoothPoints">number of points to iterate. More Points, More Smoother</param>
    /// <returns></returns>
    public static List<Vector3> CalculateLinearCurve(Vector3 StartPosition, Transform EndPosition,
        int smoothPoints = 30)
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i <= smoothPoints; i++)
        {
            Vector3 point = CalculateLinearCurve(i / (float) smoothPoints, StartPosition, EndPosition.position);
            points.Add(point);
        }

        return points;
    }

    /// <summary>
    /// Give whole points of Linear curve
    /// </summary>
    /// <param name="StartPosition">start position</param>
    /// <param name="EndPosition">end point</param>
    /// <param name="smoothPoints">number of points to iterate. More Points, More Smoother</param>
    /// <returns></returns>
    public static List<Vector3> CalculateLinearCurve(Vector3 StartPosition, Vector3 EndPosition,
        int smoothPoints = 30)
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i <= smoothPoints; i++)
        {
            Vector3 point = CalculateLinearCurve(i / (float) smoothPoints, StartPosition, EndPosition);
            points.Add(point);
        }

        return points;
    }


    private static readonly SortedDictionary<double, string> abbrevations = new SortedDictionary<double, string>
    {
        {1000, "K"},
        {10000000, "M"},
        {1000000000, "B"},
        {1000000000000, "T"},
        {1000000000000000, "QD"},
        {1000000000000000000, "QT"},
    };

    /// <summary>
    /// Abbreviate Number to 1000 to 1K etc
    /// </summary>
    /// <param name="number">number to change</param>
    /// <returns></returns>
    public static string AbbreviateNumber(float number)
    {
        for (int i = abbrevations.Count - 1; i >= 0; i--)
        {
            KeyValuePair<double, string> pair = abbrevations.ElementAt(i);
            if (Mathf.Abs(number) >= pair.Key)
            {
                double roundedNumber = number / pair.Key;
                return roundedNumber.ToString() + pair.Value;
            }
        }

        return number.ToString();
    }

    /// <summary>
    /// Extension function: LookAt object to target position
    /// </summary>
    /// <param name="trans">Current Transform</param>
    /// <param name="target">Target to Look At</param>
    /// <param name="speed">Speed of look At</param>
    /// <param name="axis">Axis to look At</param>
    public static void LookAt(this Transform trans, Vector3 target, float speed, Axis axis)
    {
        var lookPos = target - trans.position;
        switch (axis)
        {
            case Axis.X:
                lookPos.x = 0;
                break;
            case Axis.Y:
                lookPos.y = 0;
                break;
            case Axis.Z:
                lookPos.z = 0;
                break;
        }

        var rotation = Quaternion.LookRotation(lookPos);
        trans.rotation = Quaternion.Slerp(trans.rotation, rotation, Time.deltaTime * speed);
    }

    /// <summary>
    /// Extension function: LookAt object to target rotation
    /// </summary>
    /// <param name="trans">Current Transform</param>
    /// <param name="target">Target rotation to Look At</param>
    /// <param name="speed">Speed of look At</param>
    public static void LookAt(this Transform trans, Quaternion target, float speed)
    {
        trans.rotation = Quaternion.Slerp(trans.rotation, target, Time.deltaTime * speed);
    }

    /// <summary>
    /// Extension function: LookAt object to target position
    /// </summary>
    /// <param name="trans">Current Transform</param>
    /// <param name="target">Target to Look At</param>
    /// <param name="speed">Speed of look At</param>
    /// <param name="axis">Axis to look At</param>
    /// <param name="offset">Look offset</param>
    public static void LookAt(this Transform trans, Transform target, Vector3 offset, float speed, Axis axis)
    {
        var lookPos = target.position - trans.position;
        switch (axis)
        {
            case Axis.X:
                lookPos.x = 0;
                break;
            case Axis.Y:
                lookPos.y = 0;
                break;
            case Axis.Z:
                lookPos.z = 0;
                break;
        }

        if (lookPos != Vector3.zero)
        {
            var rotation = Quaternion.LookRotation(lookPos);
            rotation *= Quaternion.Euler(offset);
            trans.rotation = Quaternion.Slerp(trans.rotation, rotation, Time.deltaTime * speed);
        }
    }

    /// <summary>
    /// Extension Function : Follow target rotation by given offset
    /// </summary>
    /// <param name="trans">reference Transform</param>
    /// <param name="target">Target</param>
    /// <param name="offset">Offset</param>
    /// <param name="speed">Look Speed</param>
    public static void FollowRotation(this Transform trans, Transform target, Vector3 offset, float speed)
    {
        var rotation = target.rotation * Quaternion.Euler(offset);
        trans.rotation = Quaternion.Slerp(trans.rotation, rotation, Time.deltaTime * speed);
    }

    /// <summary>
    /// Rotate Object by given speed along given Axis
    /// </summary>
    /// <param name="trans">refernce Transform</param>
    /// <param name="speed">Speed to Rotate</param>
    /// <param name="axis">Axis to Rotate</param>
    public static void ObjectRotator(this Transform trans, float speed, Axis axis)
    {
        float _rotateDegree = 0;
        switch (axis)
        {
            case Axis.X:
                _rotateDegree = trans.localEulerAngles.x + speed * Time.deltaTime;
                _rotateDegree = _rotateDegree % 360;
                trans.localRotation =
                    Quaternion.Euler(_rotateDegree, trans.localEulerAngles.y, trans.localEulerAngles.z);
                break;
            case Axis.Y:
                _rotateDegree = trans.localEulerAngles.y + speed * Time.deltaTime;
                _rotateDegree = _rotateDegree % 360;
                trans.localRotation =
                    Quaternion.Euler(trans.localEulerAngles.x, _rotateDegree, trans.localEulerAngles.z);
                break;
            case Axis.Z:
                _rotateDegree = trans.localEulerAngles.z + speed * Time.deltaTime;
                _rotateDegree = _rotateDegree % 360;
                trans.localRotation =
                    Quaternion.Euler(trans.localEulerAngles.x, trans.localEulerAngles.y, _rotateDegree);
                break;
        }
    }

    /// <summary>
    /// Rotate Object by given speed along given Axis with given offset
    /// </summary>
    /// <param name="trans">refernce Transform</param>
    /// <param name="speed">Speed to Rotate</param>
    /// <param name="axis">Axis to Rotate</param>
    /// <param name="offset">Given Offset</param>
    public static void ObjectRotator(this Transform trans, float speed, Axis axis, Vector3 offset)
    {
        float _rotateDegree = 0;
        switch (axis)
        {
            case Axis.X:
                _rotateDegree = trans.localEulerAngles.x + speed * Time.deltaTime;
                _rotateDegree = _rotateDegree % 360;
                trans.localRotation = Quaternion.Euler(_rotateDegree + offset.x, offset.y, offset.z);
                break;
            case Axis.Y:
                _rotateDegree = trans.localEulerAngles.y + speed * Time.deltaTime;
                _rotateDegree = _rotateDegree % 360;
                trans.localRotation = Quaternion.Euler(offset.x, _rotateDegree + offset.y, offset.z);
                break;
            case Axis.Z:
                _rotateDegree = trans.localEulerAngles.z + speed * Time.deltaTime;
                _rotateDegree = _rotateDegree % 360;
                trans.localRotation = Quaternion.Euler(offset.x, offset.y, _rotateDegree);
                break;
        }
    }

    public static void FollowAnchoredPosition(this RectTransform edgeRect, float fillAmount,
        RectTransform fillAmountImage, float endMargin, Axis axis)
    {
        switch (axis)
        {
            case Axis.X:
                edgeRect.localPosition = new Vector2(fillAmount * fillAmountImage.rect.width - endMargin,
                    edgeRect.localPosition.y);
                break;
            case Axis.Y:
                edgeRect.localPosition = new Vector2(edgeRect.localPosition.x,
                    fillAmount * fillAmountImage.rect.height - endMargin);
                break;
        }
    }
}