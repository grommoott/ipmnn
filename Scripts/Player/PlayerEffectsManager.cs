using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using Saves;
using Effects;
using UI;

namespace Player
{
    public class PlayerEffectsManager : MonoBehaviour, ISaveable
    {
        private PlayerController _player;
        public PlayerController Player { get { return _player; } }

        private Dictionary<string, TemporaryEffect> _temporaryEffects = new();
        public ReadOnlyDictionary<string, TemporaryEffect> TemporaryEffects
        { get { return new ReadOnlyDictionary<string, TemporaryEffect>(_temporaryEffects); } }

        private Dictionary<string, PermanentEffect> _permanentEffects = new();
        public ReadOnlyDictionary<string, PermanentEffect> PermanentEffects
        { get { return new ReadOnlyDictionary<string, PermanentEffect>(_permanentEffects); } }

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SavesManager.Instance.IsGameLoaded);
        }

        private void Update()
        {
            if (UIManager.Instance.IsPageOpended)
            {
                return;
            }

            List<string> effectsToRemoveIds = new();

            foreach (TemporaryEffect effect in _temporaryEffects.Values)
            {
                if (effect.Update(Time.deltaTime))
                {
                    effectsToRemoveIds.Add(effect.Id);
                }
            }

            for (int i = 0; i < effectsToRemoveIds.Count; i++)
            {
                _temporaryEffects.Remove(effectsToRemoveIds[i]);
            }
        }

        public void AddTemporaryEffect(TemporaryEffect effect)
        {
            TemporaryEffect activeEffect = _temporaryEffects.GetValueOrDefault(effect.Id);

            if (activeEffect == null)
            {
                _temporaryEffects.Add(effect.Id, effect);
            }
            else
            {
                activeEffect.AddEffect(effect);
            }
        }

        public void AddPermanentEffect(PermanentEffect effect)
        {
            PermanentEffect activeEffect = _permanentEffects.GetValueOrDefault(effect.Id);


            if (activeEffect == null)
            {
                _permanentEffects.Add(effect.Id, effect);
            }
            else
            {
                activeEffect.AddEffect(effect);
            }
        }

        public TemporaryEffect GetTemporaryEffect(string id)
        {
            return _temporaryEffects.GetValueOrDefault(id);
        }

        public PermanentEffect GetPermamentEffect(string id)
        {
            return _permanentEffects.GetValueOrDefault(id);
        }

        public void RemoveTemporaryEffect(string id)
        {
            _temporaryEffects.Remove(id);
        }

        public void RemovePermamentEffect(string id)
        {
            _permanentEffects.Remove(id);
        }

        public EffectBuffs GetBuffs()
        {
            EffectBuffs buffs = new EffectBuffs();

            foreach (Effect effect in _temporaryEffects.Values)
            {
                buffs = buffs.Add(effect.GetBuffs());
            }

            foreach (Effect effect in _permanentEffects.Values)
            {
                buffs = buffs.Add(effect.GetBuffs());
            }

            return buffs;
        }

        public void Load(Newtonsoft.Json.Linq.JObject data)
        {
            if (data == null)
            {
                return;
            }

            Serialization.PlayerEffects effects = data.ToObject<Serialization.PlayerEffects>();

            foreach (Serialization.PermanentEffect effect in effects.PermanentEffects)
            {
                AddPermanentEffect(EffectsManager.GetPermanent(effect.Id, effect.Level));
            }

            foreach (Serialization.TemporaryEffect effect in effects.TemporaryEffects)
            {
                AddTemporaryEffect(EffectsManager.GetTemporary(effect.Id, effect.Level, effect.Duration));
            }
        }

        public object Save()
        {
            return new Serialization.PlayerEffects(_temporaryEffects, _permanentEffects);
        }


        public string GetSavingPath() => "playerEffects";
    }
}
