import React, { Component } from 'react';

export class FileItem extends Component {

    constructor(props) {
        super(props);
        this.state = {
            file: this.props.file
        } 
    }
    render() {
        return (
            <tr key={this.state.file.fileName} onClick={(fileName) => this.downloadFile()}>
                <td>{this.state.file.fileName}</td>
                <td>{this.state.file.directory}</td>
            </tr>
        );
    }

    downloadFile = async function(){
        
        const requestOptions = {
            method: 'POST',
            headers: {  
                //'Accept': 'application/octet-stream',
                'Content-Type': 'application/json'
            },
            //body: this.state.file
            body: JSON.stringify(this.state.file)
        };

        await fetch('filecontroller/download', requestOptions)
        .then((response) => response.blob())
        .then((blob) => {
            const url = window.URL.createObjectURL(new Blob([blob]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', this.state.file.fileName);
            // 3. Append to html page
            document.body.appendChild(link);
            // 4. Force download
            link.click();
            // 5. Clean up and remove the link
            link.parentNode.removeChild(link);
        })

        console.log(JSON.stringify(this.state.file));
    }

}
