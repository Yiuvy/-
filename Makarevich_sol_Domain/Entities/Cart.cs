using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makarevich_sol_Domain.Entities;

namespace Makarevich_sol_Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        /// <summary>
        /// Список объектов в корзине хранится в виде словаря
        /// key - идентификатор объекта
        /// </summary>
        public Dictionary<int, CartItem> CartItems { get; set; } = new();


        /// <summary>
        /// Добавить объект в корзину
        /// </summary>
        /// <param name="doctor">Добавляемый объект</param> 
        public virtual void AddToCart(Doctor doctor)
        {
            if (CartItems.ContainsKey(doctor.Id)) //если такой элемент есть
            {
                CartItems[doctor.Id].Qty++;  //увеличиваем количество
            }
            else //если его нет
            {
                CartItems.Add(doctor.Id, new CartItem  //добавляем в словарь
                {
                    Item = doctor,
                    Qty = 1

                });
            }
            ;
        }


        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="doctor">удаляемый объект</param> 
        public virtual void RemoveItems(int id)
        {
            CartItems.Remove(id);
        }


        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            CartItems.Clear();
        }


        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count { get => CartItems.Sum(item => item.Value.Qty); }



        /// <summary>
        /// Общее количество калорий
        /// </summary>
        public double TotalCalories
        {
            get => CartItems.Sum(item => item.Value.Item.AmountOfPatients * item.Value.Qty);
        }

    }
}
