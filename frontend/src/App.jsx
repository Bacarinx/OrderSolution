import './index.css'
import {BrowserRouter, Routes, Route } from "react-router-dom"
import Home from './pages/Home'
import Products from './pages/Products'
import Service from './pages/Service'
import Tabs from './pages/Tabs'
import NotFound from './pages/NotFound'


function App() {

  return (
      <Routes >
        <Route path='/' element={<Home />} />
        <Route path='/products' element={<Products />} />
        <Route path='/service' element={<Service />} />
        <Route path='/tabs' element={<Tabs />} />
        <Route path='*' element={<NotFound />} />
      </Routes>
  )
}

export default App
