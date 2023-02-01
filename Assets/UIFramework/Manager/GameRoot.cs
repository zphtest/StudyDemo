using UnityEngine;
using System.Collections;

/// <summary>
/// 作为UI启动器
/// </summary>
public class GameRoot : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //创建UI主界面
        UIManager.Instance.PushPanel(UIPanelType.MainMenu);
	}
}
