﻿using System;
using System.IO;
using System.Reflection;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;
using Terraria;


namespace Plugin
{
    [ApiVersion(2, 1)]
    public class Plugin : TerrariaPlugin
    {
        #region Plugin Info
        public override string Author => "hufang360";
        public override string Description => "好运来";
        public override string Name => "GoodLucky";
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        #endregion

        private static Config _config;
        private static readonly string savedir = Path.Combine(TShock.SavePath, "GoodLucky");

        public Plugin(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            if( !Directory.Exists(savedir) )
                Directory.CreateDirectory(savedir);
            Reload();

            ServerApi.Hooks.ServerJoin.Register(this, OnServerJoin);
            GetDataHandlers.PlayerSpawn += new EventHandler<GetDataHandlers.SpawnEventArgs>(Rebirth);
            GeneralHooks.ReloadEvent += OnReload;
        }

        private void OnReload(ReloadEventArgs args)
        {
            Reload();
            args.Player.SendSuccessMessage("已重载 好运来 配置文件");
        }

        private void Reload()
        {
            _config = Config.Load(Path.Combine(savedir,"config.json"));
        }

        // 进入时
        private void OnServerJoin(JoinEventArgs args)
        {
            TSPlayer op = TShock.Players[args.Who];
            if( _config.exclude.Contains(op.Name ) )
                return;

           SetPlayerBuff(op);
        }

        // 复活时
        private void Rebirth(object o, GetDataHandlers.SpawnEventArgs args)
        {
            TSPlayer op = args.Player;
            if( _config.exclude.Contains(op.Name ) )
                return;

            SetPlayerBuff(op);
        }

        // 设置buff
        private void SetPlayerBuff(TSPlayer op)
        {
            // Max possible buff duration as of Terraria 1.4.2.3 is 35791393 seconds (415 days).
            var timeLimit = (int.MaxValue / 60) - 1;

            foreach (BuffData d in _config.buff)
            {
                int id = d.id;
                int time = d.seconds;

                if (time < 0 || time > timeLimit)
                    time = timeLimit;

			    op.SetBuff(id, time*60);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.ServerJoin.Deregister(this, OnServerJoin);
                GetDataHandlers.PlayerSpawn -= new EventHandler<GetDataHandlers.SpawnEventArgs>(Rebirth);
                GeneralHooks.ReloadEvent -= OnReload;
            }
            base.Dispose(disposing);
        }

    }
}