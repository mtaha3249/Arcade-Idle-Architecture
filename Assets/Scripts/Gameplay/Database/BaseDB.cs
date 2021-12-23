using System;

public class BaseDB
{
}

[Serializable]
public class EconomyData
{
    public float[] Damage, DamagePrice, AttackSpeed, AttackSpeedPrice, ResourceGather, ResourceGatherPrice, World1Ground, Upgrade, Market;

    /// <summary>
    /// Push Data to local economy DB
    /// </summary>
    public void PushDataToLocalDB()
    {
        DBManager.AddEntry(DBManager.GetVariableName(() => Damage), Damage);
        DBManager.AddEntry(DBManager.GetVariableName(() => DamagePrice), DamagePrice);
        DBManager.AddEntry(DBManager.GetVariableName(() => AttackSpeed), AttackSpeed);
        DBManager.AddEntry(DBManager.GetVariableName(() => AttackSpeedPrice), AttackSpeedPrice);
        DBManager.AddEntry(DBManager.GetVariableName(() => ResourceGather), ResourceGather);
        DBManager.AddEntry(DBManager.GetVariableName(() => ResourceGatherPrice), ResourceGatherPrice);
        DBManager.AddEntry(DBManager.GetVariableName(() => World1Ground), World1Ground);
        DBManager.AddEntry(DBManager.GetVariableName(() => Upgrade), Upgrade);
        DBManager.AddEntry(DBManager.GetVariableName(() => Market), Market);
    }
}