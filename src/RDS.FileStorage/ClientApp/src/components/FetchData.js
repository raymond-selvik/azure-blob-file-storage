import React, { Component } from 'react';
import {FileItem} from './FileItem';


export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { 
      files: [], 
      loading: true 
    };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(files) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Directory</th>
          </tr>
        </thead>
        <tbody>
          {files.map(file =>
            <FileItem file={file}/>
          )}
        </tbody>
      </table>
    );
  }
  
  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.files);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('filecontroller');
    const data = await response.json();
    console.log(data);
    this.setState({ files: data, loading: false });
  }
}
