﻿using System.Collections;
using System.Collections.Generic;
using Protocol.Constant;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIMgr.Instance.Setup();

		//Debug.Log(GetCardType(new List<int>{3,3,3,4,4,4,7,9}));
		//Debug.Log(GetCardType(new List<int>{3,3,3,4,4,4,5,5,5,7,9,10}));
		//Debug.Log(GetCardType(new List<int>{3,3,3,4,4,4,5,5,5,6,6,6}));
		//Debug.Log(GetCardType(new List<int>{3,3,3,4,4,4,5,5,5,6,6,7,7,8,8}));
		//Debug.Log(GetCardType(new List<int>{3,3,3,4,4,4,5,5,5,7,7,10}));
		//Debug.Log(GetCardType(new List<int>{3,3,4,4,7,7,7,8,8,8}));
		//Debug.Log(GetCardType(new List<int>{3,3,4,7,7,7,8,8,8,9,9,9}));
		//Debug.Log(GetCardType(new List<int>{3,3,4,4,5,5,7,7,7,8,8,8,9,9,9}));
	}
	
        /// <summary>
        /// 获取卡牌类型
        /// </summary>
        /// <param name="cardList">要出的牌</param>
        public int GetCardType(List<int> cardList)
        {
            int cardType = CardType.NONE;

            switch (cardList.Count)
            {
                case 1:
                    if (IsSingle(cardList))
                    {
                        cardType = CardType.SINGLE;
                    }
                    break;
                case 2:
                    if (IsDouble(cardList))
                    {
                        cardType = CardType.DOUBLE;
                    }
                    else if (IsJokerBoom(cardList))
                    {
                        cardType = CardType.JOKER_BOOM;
                    }
                    break;
                case 3:
                    if (IsThree(cardList))
                    {
                        cardType = CardType.THREE;
                    }
                    break;
                case 4:
                    if (IsBoom(cardList))
                    {
                        cardType = CardType.BOOM;
                    }
                    else if (IsThreeAndOne(cardList))
                    {
                        cardType = CardType.THREE_ONE;
                    }
                    break;
                case 5:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    else if (IsThreeAndTwo(cardList))
                    {
                        cardType = CardType.THREE_TWO;
                    }
                    break;
                case 6:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    else if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    else if (IsTripleStraight(cardList))
                    {
                        cardType = CardType.TRIPLE_STRAIGHT;
                    }
					else if(IsTripleStraight(cardList))
						cardType = CardType.TRIPLE_STRAIGHT;
                    break;
                case 7:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    break;
                case 8:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    else if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
					else if(IsTripleStraight(cardList))
						cardType = CardType.TRIPLE_STRAIGHT;
                    break;
                case 9:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    //777 888 999 
                    else if (IsTripleStraight(cardList))
                    {
                        cardType = CardType.TRIPLE_STRAIGHT;
                    }
                    break;
                case 10:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    else if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
					else if(IsTripleStraight(cardList))
						cardType = CardType.TRIPLE_STRAIGHT;
                    break;
                case 11:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    break;
                case 12:
                    if (IsStraight(cardList))
                    {
                        cardType = CardType.STRAIGHT;
                    }
                    else if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    // 444 555 666 777
                    else if (IsTripleStraight(cardList))
                    {
                        cardType = CardType.TRIPLE_STRAIGHT;
                    }
					else if(IsTripleStraight(cardList))
						cardType = CardType.TRIPLE_STRAIGHT;
                    break;
                case 13:
                    //345678910JQKA
                    break;
                case 14:
                    if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    break;
                case 15:
                    if (IsTripleStraight(cardList))
                    {
                        cardType = CardType.TRIPLE_STRAIGHT;
                    }
					else if(IsTripleStraight(cardList))
						cardType = CardType.TRIPLE_STRAIGHT;
                    break;
                case 16:
                    if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
					else if(IsTripleStraight(cardList))
						cardType = CardType.TRIPLE_STRAIGHT;
                    break;
                case 17:
                    break;
                case 18:
                    if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
                    // 444 555 666 777 888 999 
                    else if (IsTripleStraight(cardList))
                    {
                        cardType = CardType.TRIPLE_STRAIGHT;
                    }
					else if(IsTripleStraight(cardList))
						cardType = CardType.TRIPLE_STRAIGHT;
                    break;
                case 19:
                    break;
                case 20:
                    //33 44 55 66 77 88 99 1010 JJ QQ KK AA
                    if (IsDoubleStraight(cardList))
                    {
                        cardType = CardType.DOUBLE_STRAIGHT;
                    }
					else if(IsTripleStraight(cardList))
						cardType = CardType.TRIPLE_STRAIGHT;
                    break;
                default:
                    break;
            }

            return cardType;
        }
	
        /// <summary>
        /// 是否是单牌
        /// </summary>
        /// <param name="cards">选择的手牌</param>
        /// <returns></returns>
        public static bool IsSingle(List<int> cards)
        {
            return cards.Count == 1;
        }

        /// <summary>
        /// 判断是否是对儿
        /// </summary>
        /// <param name="cards">选择的手牌</param>
        /// <returns></returns>
        public static bool IsDouble(List<int> cards)
        {
            if (cards.Count == 2)
            {
                if (cards[0] == cards[1])
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 是否是顺子
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsStraight(List<int> cards)
        {
            if (cards.Count < 5 || cards.Count > 12)
                return false;

            // 34567   45679  JQKA2
            for (int i = 0; i < cards.Count - 1; i++)
            {
                int tempWeight = cards[i];
                if (cards[i + 1] - tempWeight != 1)
                    return false;
                //不能超过A
                if (tempWeight > CardWeight.ONE || cards[i + 1] > CardWeight.ONE)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 是否是双顺
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsDoubleStraight(List<int> cards)
        {
            //  33 44 55 
            if (cards.Count < 6 || cards.Count % 2 != 0)
                return false;

            for (int i = 0; i < cards.Count - 2; i += 2)
            {
                if (cards[i] != cards[i + 1])
                    return false;
                if (cards[i + 2] - cards[i] != 1)
                    return false;
                //不能超过A
                if (cards[i] > CardWeight.ONE || cards[i + 2] > CardWeight.ONE)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 是否是飞机
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsTripleStraight(List<int> cards)
        {
            //333 444 555
            // 33344456  333444 66 77
			// 默认不是单
			if (cards == null) return false;
			
			int size = cards.Count;
			if (size < 6) return false;

			Dictionary<int,int> dic = new Dictionary<int, int>();
			for(int i=0;i<size;i++)
			{
				int card = cards[i];
				if(dic.ContainsKey(card))
				{
					dic[card]++;
				}
				else
				{
					dic.Add(card,1);
				}
			}

			int tripleCount = 0;
			List<int> removeList = new List<int>();
			foreach(var kv in dic)
			{
				if(kv.Value == 3)
				{
					tripleCount++;
					removeList.Add(kv.Key);
				}
			}
			//remove
			for(int i=0,len = removeList.Count;i<len;i++)
			{
				dic.Remove(removeList[i]);
			}

			if(dic.Count == 0 || (size-tripleCount*3) == tripleCount)
				return true;

			//判断是否有对子
			if((size-tripleCount*3) != 2*tripleCount)
				return false;

			foreach(var kv in dic)
			{
				if(kv.Value!= 2)
				{
					return false;
				}
			}

			return true;
        }

		
        /// <summary>
        /// 是否是三不带
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsThree(List<int> cards)
        {
            //333
            if (cards.Count != 3)
                return false;
            if (cards[0] != cards[1])
                return false;
            if (cards[2] != cards[1])
                return false;
            if (cards[0] != cards[2])
                return false;

            return true;
        }

        /// <summary>
        /// 是否是三带一
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsThreeAndOne(List<int> cards)
        {
            if (cards.Count != 4)
                return false;

            //5333 3335
            if (cards[0] == cards[1] && cards[1] == cards[2])
                return true;
            else if (cards[1] == cards[2] && cards[2] == cards[3])
                return true;

            return false;
        }

        /// <summary>
        /// 是否是三带二
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsThreeAndTwo(List<int> cards)
        {
            if (cards.Count != 5)
                return false;
            //33355 55333
            if (cards[0] == cards[1] && cards[1] == cards[2])
            {
                if (cards[3] == cards[4])
                    return true;
            }
            else if (cards[2] == cards[3] && cards[3] == cards[4])
            {
                if (cards[0] == cards[1])
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 判断是否是炸弹
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsBoom(List<int> cards)
        {
            if (cards.Count != 4)
                return false;
            // 0000
            if (cards[0] != cards[1])
                return false;
            if (cards[1] != cards[2])
                return false;
            if (cards[2] != cards[3])
                return false;

            return true;
        }

        /// <summary>
        /// 判断是不是王炸
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static bool IsJokerBoom(List<int> cards)
        {
            if (cards.Count != 2)
                return false;

            if (cards[0] == CardWeight.SJOKER && cards[1] == CardWeight.LJOKER)
                return true;
            else if (cards[0] == CardWeight.LJOKER && cards[1] == CardWeight.SJOKER)
                return true;

            return false;
        }


}
