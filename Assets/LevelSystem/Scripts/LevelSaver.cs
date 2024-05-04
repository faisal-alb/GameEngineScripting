using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSaver : MonoBehaviour
{
    public TMPro.TMP_InputField levelNameInput;
    public TMPro.TMP_InputField levelNumberInput;
    public TMPro.TMP_InputField rewardEarnedInput;
    public TMPro.TMP_InputField enemiesLeftInput;

    public void SaveLevel()
    {
        LevelData data = gameObject.AddComponent<LevelData>();

        data.levelName = levelNameInput.text;
        data.levelNumber = int.Parse(levelNumberInput.text);
        data.rewardEarned = int.Parse(rewardEarnedInput.text);
        data.enemiesLeft = int.Parse(enemiesLeftInput.text);

        string json = JsonUtility.ToJson(data);
        string filePath = Application.dataPath + "/Resources/Levels/Level_" + data.levelNumber.ToString("D2") + ".txt";
        
        File.WriteAllText(filePath, json);
    }
}
