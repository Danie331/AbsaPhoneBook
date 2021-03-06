import React, { useState, useEffect } from 'react';
import { forwardRef } from 'react';
import Grid from '@material-ui/core/Grid'

import MaterialTable from "material-table";
import AddBox from '@material-ui/icons/AddBox';
import ArrowDownward from '@material-ui/icons/ArrowDownward';
import Check from '@material-ui/icons/Check';
import ChevronLeft from '@material-ui/icons/ChevronLeft';
import ChevronRight from '@material-ui/icons/ChevronRight';
import Clear from '@material-ui/icons/Clear';
import DeleteOutline from '@material-ui/icons/DeleteOutline';
import Edit from '@material-ui/icons/Edit';
import FilterList from '@material-ui/icons/FilterList';
import FirstPage from '@material-ui/icons/FirstPage';
import LastPage from '@material-ui/icons/LastPage';
import Remove from '@material-ui/icons/Remove';
import SaveAlt from '@material-ui/icons/SaveAlt';
import Search from '@material-ui/icons/Search';
import ViewColumn from '@material-ui/icons/ViewColumn';
import axios from 'axios'
import Alert from '@material-ui/lab/Alert';

const tableIcons = {
    Add: forwardRef((props, ref) => <AddBox {...props} ref={ref} />),
    Check: forwardRef((props, ref) => <Check {...props} ref={ref} />),
    Clear: forwardRef((props, ref) => <Clear {...props} ref={ref} />),
    Delete: forwardRef((props, ref) => <DeleteOutline {...props} ref={ref} />),
    DetailPanel: forwardRef((props, ref) => <ChevronRight {...props} ref={ref} />),
    Edit: forwardRef((props, ref) => <Edit {...props} ref={ref} />),
    Export: forwardRef((props, ref) => <SaveAlt {...props} ref={ref} />),
    Filter: forwardRef((props, ref) => <FilterList {...props} ref={ref} />),
    FirstPage: forwardRef((props, ref) => <FirstPage {...props} ref={ref} />),
    LastPage: forwardRef((props, ref) => <LastPage {...props} ref={ref} />),
    NextPage: forwardRef((props, ref) => <ChevronRight {...props} ref={ref} />),
    PreviousPage: forwardRef((props, ref) => <ChevronLeft {...props} ref={ref} />),
    ResetSearch: forwardRef((props, ref) => <Clear {...props} ref={ref} />),
    Search: forwardRef((props, ref) => <Search {...props} ref={ref} />),
    SortArrow: forwardRef((props, ref) => <ArrowDownward {...props} ref={ref} />),
    ThirdStateCheck: forwardRef((props, ref) => <Remove {...props} ref={ref} />),
    ViewColumn: forwardRef((props, ref) => <ViewColumn {...props} ref={ref} />)
};

const api = axios.create({
    baseURL: `https://localhost:44300/`
})

function App() {

    var columns = [
        { title: "Contact Id", field: "Id", hidden: true },
        { title: "First Name", field: "FirstName", readonly: true },
        { title: "Last Name", field: "LastName", readonly: true },
        { title: "Contact Detail(s)", field: "ContactDetails", readonly: true },
        { title: " ", field: "Id", hidden: true  }
    ]
    const [data, setData] = useState([]);
    const [iserror, setIserror] = useState(false)
    const [errorMessages, setErrorMessages] = useState([])

    const handleRowAdd = (newData, resolve) => {
        api.post("/contacts", newData)
            .then(res => {
                let dataToAdd = [...data];
                dataToAdd.push(res.data.Data);
                setData(dataToAdd);
                resolve()
                setErrorMessages([])
                setIserror(false)
            })
            .catch(error => {
                setErrorMessages(["Cannot retrieve data from server"])
                setIserror(true)
                resolve()
            })
    }

    return (
        <div className="App">

            <Grid container spacing={1}>
                <Grid item xs={3}></Grid>
                <Grid item xs={6}>
                    <div>
                        {iserror &&
                            <Alert severity="error">
                                {errorMessages.map((msg, i) => {
                                    return <div key={i}>{msg}</div>
                                })}
                            </Alert>
                        }
                    </div>
                    <div style={{ maxWidth: '100%' }}>
                    <MaterialTable
                        title="Absa PhoneBook Assessment"
                        localization={{
                            body: {
                                addTooltip: "Add Contact"
                            }
                        }}
                        columns={columns}
                        data={query =>
                            new Promise((resolve, reject) => {
                                let searchField = ''
                                if (query.search) {
                                    searchField = query.search.match(/^\d/) ? `SearchNumber=${query.search}` : `SearchName=${query.search}` 
                                }
                                let url = `https://localhost:44300/contacts?${searchField}`
                                fetch(url)
                                    .then(response => response.json())
                                    .then(result => {
                                        resolve({
                                            data: result.Data,
                                            page: result.PageNumber,
                                            totalCount: result.PageSize,
                                        })
                                    })
                            })
                        }
                        icons={tableIcons}
                        editable={{
                            onRowAdd: (newData) =>
                                new Promise((resolve) => {
                                    handleRowAdd(newData, resolve)
                                })
                        }}
                        />
                    </div>
                </Grid>
                <Grid item xs={4}></Grid>
            </Grid>
        </div>
    );
}

export default App;