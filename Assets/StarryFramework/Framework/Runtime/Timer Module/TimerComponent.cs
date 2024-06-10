using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace StarryFramework
{

    [DisallowMultipleComponent]
    public class TimerComponent : BaseComponent
    {
        private TimerManager _manager = null;
        private TimerManager manager
        {
            get
            {
                if(_manager == null)
                {
                    _manager = FrameworkManager.GetManager<TimerManager>();
                }
                return _manager;
            }
        }

        [SerializeField] private TimerSettings settings;
        public List<Timer> timers => manager.timers;
        public List<TriggerTimer> triggerTimers => manager.triggerTimers;
        public List<AsyncTimer> asyncTimers => manager.asyncTimers;
        public float ClearUnusedTriggerTimersInterval => manager.ClearUnusedTriggerTimersInterval;
        public float ClearUnusedAsyncTimersInterval => manager.ClearUnusedAsyncTimersInterval;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(EditorApplication.isPlaying && _manager != null)
                (_manager as IManager).SetSettings(settings);
        }
#endif

        protected override void Awake()
        {
            base.Awake();
            if (_manager == null)
            {
                _manager = FrameworkManager.GetManager<TimerManager>();
            }
            
            (_manager as IManager).SetSettings(settings);
        }



        #region Timer

        /// <summary>
        /// ע��������ʱ����������ʱһ��Ҫͨ��DeleteTimer�������ռ�ʱ��
        /// </summary>
        /// <param Name="ignoreTimeScale">�Ƿ����ʱ�����ţ�ʹ����ʵʱ�䣩</param>
        /// <param Name="startValue">��ʱ��ʼֵ</param>
        /// <param Name="bindUpdateAction">���ڼ�ʱ����ÿ֡���õĺ���</param>
        /// <returns>��ʱ������</returns>
        public ITimer RegisterTimer(bool ignoreTimeScale = false, float startValue = 0f, UnityAction bindUpdateAction = null)
        {
            return manager.RegisterTimer(ignoreTimeScale, startValue, bindUpdateAction);
        }

        /// <summary>
        /// ����������ʱ��
        /// </summary>
        /// <param Name="timer">��Ҫ���յ�������ʱ��</param>
        public void DeleteTimer(ITimer timer)
        {
            manager.DeleteTimer(timer as Timer);
        }

        /// <summary>
        /// ע���������ʱ��
        /// </summary>
        /// <param Name="name">��ʱ������</param>
        /// <param Name="ignoreTimeScale">�Ƿ����ʱ������</param>
        /// <param Name="startValue">��ʱ��ʼֵ</param>
        public void RegisterTimer(string name, bool ignoreTimeScale = false, float startValue = 0f)
        {
            manager.RegisterTimer(name, ignoreTimeScale, startValue);
        }
        /// <summary>
        /// ���շ�������ʱ��
        /// </summary>
        /// <param Name="name"></param>
        public void DeleteTimer(string name)
        {
            manager.DeleteTimer(name);
        }

        /// <summary>
        /// ����������ʱ����Update�¼�
        /// </summary>
        /// <param Name="name"></param>
        /// <param Name="action">Update�¼���ÿ֡����һ��</param>
        public void BindUpdateAction(string name, UnityAction action)
        {
            manager.BindUpdateAction(name, action);
        }
        /// <summary>
        /// �鿴��������ʱ��״̬
        /// </summary>
        /// <param Name="name"></param>
        /// <returns></returns>
        public TimerState GetTimerState(string name)
        {
            return manager.GetTimerState(name);
        }
        /// <summary>
        /// �鿴��������ʱ��ʱ��
        /// </summary>
        /// <param Name="name"></param>
        /// <returns></returns>
        public float GetTimerTime(string name)
        {
            return manager.GetTimerTime(name);
        }
        /// <summary>
        /// ��ͣ��������ʱ��
        /// </summary>
        /// <param Name="name"></param>
        public void PauseTimer(string name)
        {
            manager.PauseTimer(name);
        }
        /// <summary>
        /// ֹͣ��ʱ�����ָ���ʼֵ��
        /// </summary>
        /// <param Name="name"></param>
        public void StopTimer(string name)
        {
            manager.StopTimer(name);
        }
        /// <summary>
        /// �ָ���ʱ��
        /// </summary>
        /// <param Name="name"></param>
        public void ResumeTimer(string name)
        {
            manager.ActivateTimer(name);
        }
        /// <summary>
        /// ������ʱ��
        /// </summary>
        /// <param Name="name"></param>
        public void StartTimer(string name)
        {
            manager.StartTimer(name);
        }
        /// <summary>
        /// ���ü�ʱ������ʼֵ
        /// </summary>
        /// <param Name="name"></param>
        public void ResetTimer(string name)
        {
            manager.ResetTimer(name);
        }

        #endregion

        #region TriggerTimer

        /// <summary>
        /// ע�ᴥ����ʱ��
        /// ��ʱ����Ϊ��������������֣�
        /// ��ע��������ʱ�������ʱ�����Զ�������
        /// ���������ʱ����ѭ���������������Զ����գ����������ʱ��ѭ����������ʹ����Ҫ��ClearUnnamedTriggerTimers()����
        /// ��ע���������ʱ�������ʱ����Ҫ��StartTriggerTimer()�ֶ���������DeleteTriggerTimer()�ֶ�����
        /// </summary>
        /// <param Name="timeDelta">��ʱ�������¼���ʱ��</param>
        /// <param Name="action">�󶨵Ĵ����¼�</param>
        /// <param Name="repeat">�Ƿ�Ϊѭ����������ÿ��timeDelta����һ���¼�</param>
        /// <param Name="name">����Ϊ������Ϊ������ʱ��</param>
        /// <param Name="ignoreTimeScale">�Ƿ����ʱ������</param>
        public void RegisterTriggerTimer(float timeDelta, UnityAction action, bool repeat = false, string name = "", bool ignoreTimeScale = false)
        {
            manager.RegisterTriggerTimer(timeDelta, action, ignoreTimeScale, repeat, name);
        }

        /// <summary>
        /// ɾ��������������ʱ��
        /// </summary>
        /// <param Name="name"></param>
        public void DeleteTriggerTimer(string name)
        {
            manager.DeleteTriggerTimer(name);
        }

        /// <summary>
        /// ��ô�����ʱ��״̬
        /// </summary>
        /// <param Name="name"></param>
        /// <returns></returns>
        public TimerState GetTriggerTimerState(string name)
        {
            return manager.GetTriggerTimerState(name);
        }

        /// <summary>
        /// ��ͣ������ʱ��
        /// </summary>
        /// <param Name="name"></param>
        public void PauseTriggerTimer(string name)
        {
            manager.PauseTriggerTimer(name);
        }

        /// <summary>
        /// �ָ�������ʱ��
        /// </summary>
        /// <param Name="name"></param>
        public void ResumeTriggerTimer(string name)
        {
            manager.ActivateTriggerTimer(name);
        }

        /// <summary>
        /// ֹͣ������ʱ�������ָ���ʼֵ��
        /// </summary>
        /// <param Name="name"></param>
        public void StopTriggerTimer(string name)
        {
            manager.StopTriggerTimer(name);
        }

        /// <summary>
        /// ����������ʱ��
        /// </summary>
        /// <param Name="name"></param>
        public void StartTriggerTimer(string name)
        {
            manager.StartTriggerTimer(name);
        }

        /// <summary>
        /// �����������������ʱ��
        /// </summary>
        public void ClearUnnamedTriggerTimers()
        {
            manager.ClearUnnamedTriggerTimers();
        }


        #endregion

        #region AsyncTimer
        /// <summary>
        /// ע���첽������ʱ��
        /// ��ʱ����Ϊ��������������֣�
        /// ��ע��������ʱ�������ʱ�����Զ�������
        /// ���������ʱ����ѭ���������������Զ����գ����������ʱ��ѭ����������ʹ����Ҫ��ClearUnnamedAsyncTimers()����
        /// ��ע���������ʱ�������ʱ����Ҫ��StartAsyncTimer()�ֶ���������DeleteAsyncTimer()�ֶ�����
        /// </summary>
        /// <param Name="timeDelta">��ʱ�������¼���ʱ��</param>
        /// <param Name="action">�󶨵Ĵ����¼�</param>
        /// <param Name="repeat">�Ƿ�Ϊѭ����������ÿ��timeDelta����һ���¼�</param>
        /// <param Name="name">����Ϊ������Ϊ������ʱ��</param>
        public void RegisterAsyncTimer(float timeDelta, UnityAction action, bool repeat = false, string name = "")
        {
            manager.RegisterAsyncTimer(timeDelta,action,repeat,name);
        }

        /// <summary>
        /// ɾ���������첽������ʱ��
        /// </summary>
        /// <param Name="name"></param>
        internal void DeleteAsyncTimer(string name)
        {
            manager.DeleteAsyncTimer(name);
        }

        /// <summary>
        /// ����첽������ʱ��״̬
        /// </summary>
        /// <param Name="name"></param>
        /// <returns></returns>
        public TimerState GetAsyncTimerState(string name)
        {
            return manager.GetAsyncTimerState(name);
        }

        /// <summary>
        /// �����첽������ʱ��
        /// </summary>
        /// <param Name="name"></param>
        public void StartAsyncTimer(string name)
        {
            manager.StartAsyncTimer(name);
        }

        /// <summary>
        /// ֹͣ�첽������ʱ��
        /// </summary>
        /// <param Name="name"></param>
        public void StopAsyncTimer(string name)
        {
            manager.StopAsyncTimer(name);
        }

        /// <summary>
        /// ������������첽������ʱ��
        /// </summary>
        public void ClearUnnamedAsyncTimers()
        {
            manager.ClearUnnamedAsyncTimers();
        }

        #endregion

    }
}

