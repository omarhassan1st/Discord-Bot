using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.IO;


namespace Discord
{
    public class Commandss : ModuleBase<SocketCommandContext>
    {
        SqlMain SQLNEEDED = new SqlMain();

        #region (Status)
        /*[Command("status")]
        public async Task SqlStatus(string charname)
        {
            try
            {
                SQLNEEDED.CharName16 = charname;
                await Task.Run(new Action(SQLNEEDED.DB_SHARD__Char));
                await ReplyAsync($"Char Name: {charname}\nlvl: {SQLNEEDED.PlayerLvl}\nStr: {SQLNEEDED.Str}\nInt: {SQLNEEDED.Int}\nZerkPoitns: {SQLNEEDED.ZerkPoints}\nLastLogOut: {SQLNEEDED.LastLogOut}\n -----------------------------------------");
            }
            catch
            {
                  await ReplyAsync($"Wrong CharName");
            }
        }*/
        #endregion

        [Command("active")]
        public async Task Global()
        {
            try
            {
                while (true)
                {
                    await Task.Run(new Action(SQLNEEDED.SR__Global));
                    if (SQLNEEDED.Content != SQLNEEDED.LastContant || SQLNEEDED.Sendtime != SQLNEEDED.LastSendTime)
                    {
                        SQLNEEDED.LastSendTime = SQLNEEDED.Sendtime;
                        SQLNEEDED.LastContant = SQLNEEDED.Content;
                        ITextChannel logChannel = Context.Client.GetChannel(Program.Global) as ITextChannel;
                        var EmbedBuilderLog = new EmbedBuilder()
                            .WithDescription($"{SQLNEEDED.Sender}: {SQLNEEDED.Content} , [{SQLNEEDED.Sendtime}]");
                        Embed embedLog = EmbedBuilderLog.Build();
                        await logChannel.SendMessageAsync(embed: embedLog);
                    }

                    await Task.Run(new Action(SQLNEEDED.SR__UniqueKills));
                        if (SQLNEEDED.UniqueName != SQLNEEDED.LastUniqueKilled || SQLNEEDED.KilledTime != SQLNEEDED.LastKilledTime)
                        {
                            SQLNEEDED.LastUniqueKilled = SQLNEEDED.UniqueName;
                            SQLNEEDED.LastKilledTime = SQLNEEDED.KilledTime;
                            ITextChannel logChannel = Context.Client.GetChannel(Program.Unique) as ITextChannel;
                            var EmbedBuilderLog = new EmbedBuilder()
                                .WithDescription($"[{SQLNEEDED.KillerName}] Has Killed [{SQLNEEDED.UniqueName}] , [{SQLNEEDED.KilledTime}]");
                            Embed embedLog = EmbedBuilderLog.Build();
                            await logChannel.SendMessageAsync(embed: embedLog);
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
