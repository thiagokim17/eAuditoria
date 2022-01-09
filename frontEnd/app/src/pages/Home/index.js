import React  from "react";
import "../../style.css";

import { Link } from 'react-router-dom';

const Home = () => {
  return (
      <div id="page-home">
                  <h1>Desafio proposto pela eAuditoria</h1>

                  <Link to="/cliente">
                      <strong>Crud Cliente</strong>
                  </Link>

                  <br/>

                  <Link to="/locacao">
                      <strong>Crud Locação</strong>
                  </Link>
      </div>
  )
}

export default Home;