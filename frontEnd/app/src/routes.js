import React from 'react';
import { Route, BrowserRouter } from 'react-router-dom';

import Clientes from "./pages/Clientes";
import Locacao from "./pages/Locacao";
import Home from "./pages/Home";

const Routes = () => {
    return (
        <BrowserRouter>
          <Route path="/" exact component={Home} exact />
          <Route path="/cliente" component={Clientes} />

          <Route path="/locacao" component={Locacao} />
        </BrowserRouter>
    )
};

export default Routes;
