import React, { Component } from 'react';
import { BiFileBlank } from "react-icons/bi";

export class FileItem extends Component {

    constructor(props) {
        super(props);
        this.state = {
            file: this.props.file
        } 
    }
    render() {
        return (
            <tr key={this.state.file.fileName}>
                <td><BiFileBlank/>{this.state.file.fileName}</td>
                <td>{this.state.file.directory}</td>
            </tr>
        );
    }
}
