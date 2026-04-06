using UnityEngine;

// sets condition value
// gets condition value to check if action is possible
public interface IConditionService
{
    public void Set(string uID, bool value);
    public bool Get(string uID);
    public bool CheckAll(string[] uIDs);
}
