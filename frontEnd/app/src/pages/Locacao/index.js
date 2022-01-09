import 'antd/dist/antd.css';
import React, { useState, useEffect } from 'react';

import api from '../../services/api'

import { Table, Button, Modal, Input, DatePicker, Space, Select } from 'antd';

import { EditOutlined, DeleteOutlined } from '@ant-design/icons'

import moment from 'moment';

const { Option } = Select;

export default function Locacao() {
  const [opeModal, setOpenModal] = useState(false)
  const [locacaoUpdate, setLocacaoUpdate] = useState(false)
  const [data, setData] = useState(null)

  const [clientes, setClientes] = useState(null)
  const [filmes, setFilmes] = useState(null)


  const coluns = [
    {
      key: '1',
      title: 'ID',
      dataIndex: 'id'
    },
    {
      key: '2',
      title: 'Id Cliente',
      dataIndex: 'idCliente'
    },
    {
      key: '3',
      title: 'Nome Cliente',
      dataIndex: 'nomeCliente'
    },
    {
      key: '4',
      title: 'Id Filme',
      dataIndex: 'idFilme'
    },
    {
      key: '5',
      title: 'Titulo Filme',
      dataIndex: 'tituloFilme'
    },
    {
      key: '6',
      title: 'Data Locacao',
      dataIndex: 'dataLocacao'
    },
    {
      key: '7',
      title: 'Data Devolucao',
      dataIndex: 'dataDevolucao'
    },
    {
      key: '8',
      title: 'Action',
      render: (record) => {
        return <>
          <EditOutlined onClick={() => { onEditLocacao(record) }} />
          <DeleteOutlined onClick={() => { onDeleteLocacao(record) }} style={{ color: "red", marginLeft: 12 }} />
        </>
      }
    }
  ]

  const getDados = () => {
    api.get('api/Locacao')
      .then(response => {
        setData(response.data.map(dado => {
          return {
            ...dado,
            dataLocacaoFormat: moment(dado.dataLocacao, 'DD/MM/YYYY'),
            dataDevolucaoFormat: dado.dataDevolucao ? moment(dado.dataDevolucao, 'DD/MM/YYYY') : null
          }
        }));
      })
  }

  useEffect(() => {
    getDados();

    api.get('api/Cliente')
      .then(response => {
        setClientes(response.data);
      })

    api.get('api/Filme')
      .then(response => {
        setFilmes(response.data);
      })

  }, [])

  const onAddLocacao = () => {
    setOpenModal(true);
  }

  const onDeleteLocacao = (record) => {
    Modal.confirm({
      title: 'Confirmar exclusão do registro?',
      okType: 'danger',
      onOk: () => {
        api.delete(`api/Locacao/${record.id}`).then(res => getDados());
      }
    })
  }

  const onEditLocacao = (record) => {
    setOpenModal(true);
    setLocacaoUpdate({ ...record });
  }

  const resetUpdate = () => {
    setOpenModal(false);
    setLocacaoUpdate(null);
  }

  function onChangeCliente(value) {
    setLocacaoUpdate(pre => {
      return { ...pre, idCliente: value }
    })
  }

  function onChangeFilme(value) {
    setLocacaoUpdate(pre => {
      return { ...pre, idFilme: value }
    })
  }

  const dateFormatList = ['DD/MM/YYYY', 'DD/MM/YY'];

  return (
    <div className='App'>
      <header className='App-header'></header>
      <Table
        columns={coluns}
        dataSource={data}
      />
      <Button onClick={onAddLocacao}>Add new</Button>

      <Modal
        title='Locacao'
        visible={opeModal}
        okText='Salvar'
        onCancel={() => {
          resetUpdate();
        }}
        onOk={() => {
          if (locacaoUpdate.id)
            api.put('api/Locacao', locacaoUpdate).then(res => getDados());
          else {
            locacaoUpdate.id = 0;
            api.post('api/Locacao', locacaoUpdate).then(res => getDados());
          }
          resetUpdate();
        }}
      >

        <Space direction="vertical">

          <Select
            value={locacaoUpdate?.idCliente}
            showSearch
            placeholder="Cliente"
            optionFilterProp="children"
            onChange={onChangeCliente}
            filterOption={(input, option) =>
              option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
            }
          >
            {
              clientes &&
              clientes.map(cli => {
                return (<Option key={cli.id} value={cli.id}>{cli.nome}</Option>)
              })
            }
          </Select>

          <Select
            value={locacaoUpdate?.idFilme}
            showSearch
            placeholder="Filme"
            optionFilterProp="children"
            onChange={onChangeFilme}
            filterOption={(input, option) =>
              option.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
            }
          >
            {
              filmes &&
              filmes.map(filme => {
                return (<Option key={filme.id} value={filme.id}>{filme.tituto}</Option>)
              })
            }
          </Select>

          <DatePicker
            value={locacaoUpdate?.dataLocacaoFormat}
            format='DD/MM/YYYY'
            onChange={(date, dateString) => {

              setLocacaoUpdate(pre => {
                console.log(dateString)
                return { ...pre, dataLocacao: dateString, dataLocacaoFormat: date }
              })
            }}

          />

          <DatePicker
            value={locacaoUpdate?.dataDevolucaoFormat}
            format='DD/MM/YYYY'
            onChange={(date, dateString) => {

              setLocacaoUpdate(pre => {
                return { ...pre, dataDevolucao: dateString, dataDevolucaoFormat: date }
              })
            }}
          />

        </Space>
      </Modal>
    </div>
  );
}