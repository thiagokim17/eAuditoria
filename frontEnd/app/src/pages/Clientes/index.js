import 'antd/dist/antd.css';
import React, { useState, useEffect } from 'react';

import api from '../../services/api'

import { Table, Button, Modal, Input, DatePicker, Space } from 'antd';

import { EditOutlined, DeleteOutlined } from '@ant-design/icons'

import moment from 'moment';

export default function Cliente() {
  const [opeModal, setOpenModal] = useState(false)
  const [clienteUpdate, setClienteUpdate] = useState(false)
  const [data, setData] = useState(null)

  const coluns = [
    {
      key: '1',
      title: 'ID',
      dataIndex: 'id'
    },
    {
      key: '2',
      title: 'Nome',
      dataIndex: 'nome'
    },
    {
      key: '3',
      title: 'CPF',
      dataIndex: 'cpf'
    },
    {
      key: '4',
      title: 'Data Nascimento',
      dataIndex: 'dataNascimento'
    },
    {
      key: '5',
      title: 'Action',
      render: (record) => {
        return <>
          <EditOutlined onClick={() => { onEditCliente(record) }} />
          <DeleteOutlined onClick={() => { onDeleteCliente(record) }} style={{ color: "red", marginLeft: 12 }} />
        </>
      }
    }
  ]

  const getDados = () => {
    api.get('api/Cliente')
      .then(response => {
        setData(response.data.map(dado => {
          return {...dado, dataNascFormat: moment(dado.dataNascimento, 'DD/MM/YYYY')}
        }));
      })
  }

  useEffect(() => {
    getDados();
  }, [])

  const onAddCliente = () => {
    setOpenModal(true);
  }

  const onDeleteCliente = (record) => {
    Modal.confirm({
      title: 'Confirmar exclusão do registro?',
      okType: 'danger',
      onOk: () => {
        api.delete(`api/Cliente/${record.id}`).then(res => getDados());
      }
    })
  }

  const onEditCliente = (record) => {
    setOpenModal(true);
    setClienteUpdate({ ...record });
  }

  const resetUpdate = () => {
    setOpenModal(false);
    setClienteUpdate(null);
  }

  return (
    <div className='App'>
      <header className='App-header'></header>
      <Table
        columns={coluns}
        dataSource={data}
      />
      <Button onClick={onAddCliente}>Add new</Button>

      <Modal
        title='Cliente'
        visible={opeModal}
        okText='Salvar'
        onCancel={() => {
          resetUpdate();
        }}
        onOk={() => {
          if (clienteUpdate.id)
            api.put('api/Cliente', clienteUpdate).then(res => getDados());
          else {
            clienteUpdate.id = 0;
            api.post('api/Cliente', clienteUpdate).then(res => getDados());
          }
          resetUpdate();
        }}
      >

        <Space direction="vertical">
          <Input
            value={clienteUpdate?.nome}
            onChange={(e) => {
              setClienteUpdate(pre => {
                return { ...pre, nome: e.target.value }
              })
            }} />
          <Input
            value={clienteUpdate?.cpf}
            onChange={(e) => {
              setClienteUpdate(pre => {
                return { ...pre, cpf: e.target.value }
              })
            }} />

          <DatePicker
            value={clienteUpdate?.dataNascFormat}
            format='DD/MM/YYYY'
            onChange={(date, dateString) => {

              setClienteUpdate(pre => {
                return { ...pre, dataNascimento: dateString, dataNascFormat:  date}
              })
            }}

          />
        </Space>
      </Modal>
    </div>
  );
}