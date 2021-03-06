﻿using AhpilyServer;
using Protocol.Constant;
using Protocol.Dto.Fight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Cache.Fight
{
    public class FightRoom
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 战斗中的的用户id 对应的连接对象
        /// </summary>
        public Dictionary<int, ClientPeer> UIdClientDict { get; private set; }

        //public Dictionary<int, List<CardDto>> LeaveUIdCardDict { get; set; }

        /// <summary>
        /// 存储所有玩家
        /// </summary>
        public List<PlayerDto> PlayerList { get; private set; }

        /// <summary>
        /// 中途离开用户列表
        /// </summary>
        public List<int> LeaveUIdList { get; private set; }

        /// <summary>
        /// 牌库
        /// </summary>
        public LibraryModel libraryModel { get; set; }

        /// <summary>
        /// 底牌
        /// </summary>
        public List<CardDto> TableCardList { get; set; }

        /// <summary>
        /// 倍数
        /// </summary>
        public int Multiple { get; set; }

        public RoundModel roundModel { get; set; }

        /// <summary>
        /// 构造方法 初始化
        /// </summary>
        /// <param name="id"></param>
        public FightRoom(int id, List<int> uidList)
        {
            this.Id = id;
            this.PlayerList = new List<PlayerDto>();
            foreach (int uid in uidList)
            {
                PlayerDto player = new PlayerDto(uid);
                this.PlayerList.Add(player);
            }
            this.LeaveUIdList = new List<int>();
            this.libraryModel = new LibraryModel();
            this.TableCardList = new List<CardDto>();
            this.Multiple = 1;
            this.roundModel = new RoundModel();
            this.UIdClientDict = new Dictionary<int, ClientPeer>();
        }

        /// <summary>
        /// 开始战斗
        /// </summary>
        public void StartFight(int userId, ClientPeer client)
        {
            UIdClientDict.Add(userId, client);
        }

        public void Init(List<int> uidList)
        {
            foreach (int uid in uidList)
            {
                PlayerDto player = new PlayerDto(uid);
                this.PlayerList.Add(player);
            }
        }

        public bool IsOffline(int uid)
        {
            return LeaveUIdList.Contains(uid);
        }

        /// <summary>
        /// 转换出牌
        /// </summary>
        public int Turn()
        {
            int currUId = roundModel.CurrentUId;
            int nextUId = GetNextUId(currUId);
            //更改回合信息
            roundModel.CurrentUId = nextUId;
            return nextUId;
        }

        //计算下一个出牌者
        public int GetNextUId(int currUId)
        {
            for (int i = 0; i < PlayerList.Count; i++)
            {
                if (PlayerList[i].UserId == currUId)
                {
                    if (i == 2)
                        return PlayerList[0].UserId;
                    else
                        return PlayerList[i + 1].UserId;
                }
            }
            throw new Exception("并没有出牌者！");
        }

        /// <summary>
        /// 判断能不能压上一回合的牌
        /// </summary>
        /// <param name="type"></param>
        /// <param name="weight"></param>
        /// <param name="length"></param>
        /// <param name="userId"></param>
        /// <param name="cardList"></param>
        /// <returns></returns>
        public bool DeadCard(int type, int weight, int length, int userId, List<CardDto> cardList)
        {
            bool canDeal = false;
            if (type == roundModel.LastCardType && weight > roundModel.LastWeight)
            {
                //用什么牌管什么牌
                if (type == roundModel.LastCardType && weight > roundModel.LastWeight)
                {
                    //特殊的类型：顺子
                    if (type == CardType.STRAIGHT || type == CardType.DOUBLE_STRAIGHT || type == CardType.TRIPLE_STRAIGHT)
                    {
                        //长度限制
                        if (length == roundModel.LastLength)
                        {
                            canDeal = true;
                        }
                    }
                    else
                    {
                        canDeal = true;
                    }
                }

            }
            else if (type == CardType.BOOM && roundModel.LastCardType != CardType.BOOM)//上一个不是炸弹，出炸弹，可以出牌
            {
                canDeal = true;
            }
            else if (type == CardType.JOKER_BOOM)//出王炸也可以出牌
            {
                canDeal = true;
            }
            else if (userId == roundModel.BiggestUId)//当前id是当前回合最大出牌者
            {
                canDeal = true;
            }


            //出牌
            if (canDeal)
            {
                //移除手牌
                RemoveCards(userId, cardList);
                //可能翻倍
                if (type == CardType.BOOM)
                {
                    Multiple *= 2;
                }
                else if (type == CardType.JOKER_BOOM)
                {
                    Multiple *= 4;
                }
                //保存回合信息
                roundModel.Change(length, type, weight, userId);
            }
            return canDeal;
        }

        /// <summary>
        /// 移除玩家手牌
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardList"></param>
        public void RemoveCards(int userId, List<CardDto> cardList)
        {
            //获取玩家现有手牌
            List<CardDto> currList = getUserCards(userId);

            List<CardDto> list = new List<CardDto>();
            foreach (var select in cardList)
            {
                for (int i = currList.Count - 1; i >= 0; i--)
                {
                    if (currList[i].Name == select.Name)
                    {
                        list.Add(currList[i]);
                        break;
                    }
                }
            }
            foreach (var card in list)
            {
                currList.Remove(card);
            }
        }

        /// <summary>
        /// 获取玩家的现有手牌
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CardDto> getUserCards(int userId)
        {
            foreach (PlayerDto player in PlayerList)
            {
                if (player.UserId == userId)
                    return player.CardList;
            }
            throw new Exception("不存在这个玩家");
        }



        /// <summary>
        /// 返回玩家的第一张手牌
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CardDto> getUserFirstCard(int userId)
        {
            List<CardDto> cards = new List<CardDto>();
            CardDto card = getUserCards(userId)[0];
            cards.Add(card);
            return cards;
        }

        /// <summary>
        /// 发牌(初始化角色手牌)
        /// </summary>
        public void InitPlayerCards()
        {
            for (int i = 0; i < 17; i++)
            {
                CardDto card = libraryModel.Deal();
                PlayerList[0].Add(card);
            }
            for (int i = 0; i < 17; i++)
            {
                CardDto card = libraryModel.Deal();
                PlayerList[1].Add(card);
            }
            for (int i = 0; i < 17; i++)
            {
                CardDto card = libraryModel.Deal();
                PlayerList[2].Add(card);
            }
            for (int i = 0; i < 3; i++)
            {
                CardDto card = libraryModel.Deal();
                TableCardList.Add(card);
            }
        }

        /// <summary>
        /// 设置地主身份
        /// </summary>
        public void SetLandlord(int userId)
        {
            foreach (PlayerDto player in PlayerList)
            {
                if (player.UserId == userId)
                {
                    //找对人了
                    player.Identity = Identity.LANDLORD;
                    //给地主发底牌
                    for (int i = 0; i < TableCardList.Count; i++)
                    {
                        player.Add(TableCardList[i]);
                    }
                    //重新排序
                    this.Sort();
                    //开始回合
                    roundModel.Start(userId);
                }
            }
        }

        /// <summary>
        /// 获取玩家的数据模型
        /// </summary>
        /// <returns></returns>
        public PlayerDto GetPlayerModel(int userId)
        {
            foreach (PlayerDto player in PlayerList)
            {
                if (player.UserId == userId)
                {
                    return player;
                }
            }
            throw new Exception("没有这个玩家！获取不到数据");
        }

        public int GetPlayerIdeentity(int userId)
        {
            return GetPlayerModel(userId).Identity;
            throw new Exception("没有这个玩家！获取不到数据");
        }

        /// <summary>
        /// 获取相同身份的用户id
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public List<int> GetSameIdentityUIds(int identity)
        {
            List<int> uIds = new List<int>();
            foreach (PlayerDto player in PlayerList)
            {
                if (player.Identity == identity)
                {
                    uIds.Add(player.UserId);
                }
            }
            return uIds;
        }

        /// <summary>
        /// 获取不相同身份的用户id
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public List<int> GetDifferentIdentityUIds(int identity)
        {
            List<int> uIds = new List<int>();
            foreach (PlayerDto player in PlayerList)
            {
                if (player.Identity != identity)
                {
                    uIds.Add(player.UserId);
                }
            }
            return uIds;
        }

        /// <summary>
        /// 获取房间内第一个玩家的id
        /// </summary>
        /// <returns></returns>
        public int GetFirstUId()
        {
            return PlayerList[0].UserId;
        }

        /// <summary>
        /// 排序手牌
        /// </summary>
        /// <param name="cardList"></param>
        /// <param name="asc"></param>
        public void sortCard(List<CardDto> cardList, bool asc = true)
        {
            cardList.Sort(
                delegate (CardDto a, CardDto b)
                {
                    if (asc)
                        return a.Weight.CompareTo(b.Weight);
                    else
                        return a.Weight.CompareTo(b.Weight) * -1;
                });
        }

        /// <summary>
        /// 默认升序
        /// </summary>
        /// <param name="asc"></param>
        public void Sort(bool asc = true)
        {
            sortCard(PlayerList[0].CardList, asc);
            sortCard(PlayerList[1].CardList, asc);
            sortCard(PlayerList[2].CardList, asc);
        }

        /// <summary>
        /// 广播房间内所有玩家的信息
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="subCode"></param>
        /// <param name="value"></param>
        public void Brocast(int opCode, int subCode, object value, ClientPeer exClient = null)
        {
            SocketMessage msg = new SocketMessage(opCode, subCode, value);
            byte[] data = EncodeTool.EncodeMsg(msg);
            byte[] packet = EncodeTool.EncodePacket(data);

            foreach (var client in UIdClientDict.Values)
            {
                if (client == exClient)
                    continue;
                client.Send(packet);
            }
        }
    }
}
