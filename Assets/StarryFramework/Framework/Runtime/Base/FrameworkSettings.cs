using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace StarryFramework
{
    [Serializable]
    internal class FrameworkSettings
    {

        internal int FrameworkSceneID = -1;

        [Header("��Ϸ��ܵ�Log�ȼ�")] [Space(5)] 
        [SerializeField] internal FrameworkDebugType debugType = FrameworkDebugType.Normal;
        
        [Header("����ڲ��¼�")] [Tooltip("����ڲ��¼�������ʱ���Ƿ��ͬʱ�����ⲿͬ���¼�")] [Space(5)] 
        [SerializeField] internal bool InternalEventTrigger = true;
        
        [Header("��ʼ��������")] [Tooltip("��Ϸ�������صĳ�ʼ�����������GameFramework�򲻼���")] [Space(5)] 
        [SerializeField] [SceneIndex] internal int StartScene = 0;

        [Tooltip("��ʼ���������Ƿ�����Ĭ�϶���")]
        [SerializeField] internal bool StartSceneAnimation = false;
        
        [Header("��Ϸ������õ�ģ��")] [Space(5)]
        [Tooltip("��Ϸ��ܸ�ģ���Ƿ������Լ����ȼ���Խ�����б�ǰ�����ȼ�Խ��")]
        [SerializeField] internal List<ModuleType> modules= new List<ModuleType>();

        internal bool ModuleInUse(ModuleType type)
        {
            return modules.Contains(type);
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        internal void Init()
        {
#if UNITY_EDITOR

            var scenes = EditorBuildSettings.scenes;

            for (int i = 0; i < scenes.Length; i++)
            {

                string sceneName = Utilities.ScenePathToName(scenes[i].path);

                if (sceneName == "GameFramework")
                {
                    FrameworkSceneID = i;
                    break;
                }
            }
            if (FrameworkSceneID == -1)
            {
                Debug.LogError("Check Your Build Settings: You need to add the scene \"GameFramework\".");
            }
#else

            FrameworkSceneID = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
#endif
        }

        /// <summary>
        /// ���ú����Լ��
        /// </summary>
        internal void SettingCheck()
        {
            List<ModuleType> check = new List<ModuleType>();
            foreach (ModuleType type in modules)
            {
                if (check.Contains(type))
                {
                    Debug.LogError("Same components are not allowed in the Mpdule List");
                }
                else
                {
                    check.Add(type);
                }
            }
            check.Clear();
        }

    }
}
