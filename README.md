Вот пошаговый план реализации корзины с использованием Redux для вашего проекта:

1. Установка Redux

Если Redux ещё не установлен, установите необходимые пакеты:

npm install @reduxjs/toolkit react-redux

2. Создание Redux Slice для корзины

Создайте файл basketSlice.js в папке store (или подходящем месте).

import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  items: [], // Список блюд в корзине
  totalPrice: 0, // Общая сумма корзины
};

const basketSlice = createSlice({
  name: 'basket',
  initialState,
  reducers: {
    addToBasket(state, action) {
      const dish = action.payload; // Получаем добавляемое блюдо
      const existingItem = state.items.find(item => item.id === dish.id);
      
      if (existingItem) {
        // Если блюдо уже есть в корзине, увеличиваем количество
        existingItem.quantity += 1;
        existingItem.totalPrice += dish.price;
      } else {
        // Если блюдо новое, добавляем его в список
        state.items.push({ ...dish, quantity: 1, totalPrice: dish.price });
      }
      state.totalPrice += dish.price; // Увеличиваем общую сумму корзины
    },
    removeFromBasket(state, action) {
      const id = action.payload;
      const itemIndex = state.items.findIndex(item => item.id === id);

      if (itemIndex >= 0) {
        const item = state.items[itemIndex];
        state.totalPrice -= item.totalPrice;
        state.items.splice(itemIndex, 1);
      }
    },
    clearBasket(state) {
      state.items = [];
      state.totalPrice = 0;
    },
  },
});

export const { addToBasket, removeFromBasket, clearBasket } = basketSlice.actions;
export default basketSlice.reducer;

3. Настройка Redux Store

Создайте store.js (если его ещё нет) и подключите basketSlice.

import { configureStore } from '@reduxjs/toolkit';
import basketReducer from './basketSlice';

export const store = configureStore({
  reducer: {
    basket: basketReducer,
  },
});

4. Подключение Redux к React

Обёрните приложение в Provider в index.js.

import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { store } from './store';
import App from './App';

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById('root')
);

5. Добавление логики в компоненты

5.1 Компонент Dish

Добавьте обработчик для кнопки "Добавить":

import React from 'react';
import { useDispatch } from 'react-redux';
import { addToBasket } from '../store/basketSlice';

const Dish = ({ dish }) => {
  const dispatch = useDispatch();

  const handleAddToBasket = () => {
    dispatch(addToBasket(dish));
  };

  return (
    <div className="dish-card">
      <h3>{dish.name}</h3>
      <p>Цена: {dish.price} ₽</p>
      <button onClick={handleAddToBasket}>Добавить</button>
    </div>
  );
};

export default Dish;

5.2 Компонент NavMenu

Добавьте ссылку на страницу корзины:

import React from 'react';
import { useSelector } from 'react-redux';

const NavMenu = () => {
  const totalItems = useSelector(state =>
    state.basket.items.reduce((total, item) => total + item.quantity, 0)
  );

  return (
    <nav>
      <a href="/">Главная</a>
      <a href="/basket">
        Корзина ({totalItems})
      </a>
    </nav>
  );
};

export default NavMenu;

5.3 Компонент BasketPage

Отобразите содержимое корзины:

import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { removeFromBasket, clearBasket } from '../store/basketSlice';

const BasketPage = () => {
  const { items, totalPrice } = useSelector(state => state.basket);
  const dispatch = useDispatch();

  const handleRemove = (id) => {
    dispatch(removeFromBasket(id));
  };

  const handleClear = () => {
    dispatch(clearBasket());
  };

  return (
    <div>
      <h1>Корзина</h1>
      {items.length === 0 ? (
        <p>Корзина пуста</p>
      ) : (
        <div>
          <ul>
            {items.map(item => (
              <li key={item.id}>
                {item.name} - {item.quantity} x {item.price} ₽ = {item.totalPrice} ₽
                <button onClick={() => handleRemove(item.id)}>Удалить</button>
              </li>
            ))}
          </ul>
          <h3>Общая сумма: {totalPrice} ₽</h3>
          <button onClick={handleClear}>Очистить корзину</button>
          <button>Заказать</button>
        </div>
      )}
    </div>
  );
};

export default BasketPage;

6. Навигация между страницами

Если вы используете React Router, настройте маршруты:

import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavMenu from './components/NavMenu';
import DishList from './components/DishList';
import BasketPage from './components/BasketPage';

function App() {
  return (
    <Router>
      <NavMenu />
      <Routes>
        <Route path="/" element={<DishList />} />
        <Route path="/basket" element={<BasketPage />} />
      </Routes>
    </Router>
  );
}

export default App;

Теперь у вас есть полностью функционирующая корзина с использованием Redux!

